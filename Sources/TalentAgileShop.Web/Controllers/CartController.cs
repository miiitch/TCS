using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using TalentAgileShop.Model;

namespace TalentAgileShop.Web.Controllers
{
    [System.Web.Http.RoutePrefix("_api/cart")]
    public class CartController : ApiController
    {
        private readonly ICartRepository _cartRepository;
        private readonly IDataContext _dataContext;
        private readonly ICartPriceCalculator _priceCalculator;

        public CartController(ICartRepository cartRepository, IDataContext dataContext, ICartPriceCalculator priceCalculator)
        {
            _cartRepository = cartRepository;
            _dataContext = dataContext;
            _priceCalculator = priceCalculator;
        }

        private string GetCookieId()
        {
            var cookie = Request.Headers.GetCookies("cart-id").FirstOrDefault();

            if (cookie == null)
            {
                return Guid.NewGuid().ToString();
            }
            var value = cookie.Cookies.FirstOrDefault()?.Value;

            return value;
        }

        [System.Web.Http.Route("")]
        public HttpResponseMessage Get()
        {
            var id = GetCookieId();


            var cart = _cartRepository.Get(id);

            if (cart == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            else
            {
                return Cart(id, cart);
            }

        }



        [System.Web.Http.Route("")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage CartAction([FromBody] CartAction action)
        {
            if ((action == null) || (action.ProductId == null) || (action.Command == null))
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            var id = GetCookieId();

            Model.Cart cart;
            switch (action.Command)
            {
                case "add":
                    cart = _cartRepository.AddProduct(id, action.ProductId);
                    break;
                case "remove":
                    cart = _cartRepository.RemoveProduct(id, action.ProductId);
                    break;
                case "delete":
                    cart = _cartRepository.DeleteProduct(id, action.ProductId);
                    break;
                default:
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            return Cart(id, cart);
        }


        private HttpResponseMessage Cart(string id, Model.Cart cart)
        {

            var response = Request.CreateResponse<Model.Cart>(
        HttpStatusCode.OK,
        cart
    );
            
            var cookie = new CookieHeaderValue("cart-id", id);
            
            cookie.Expires = DateTimeOffset.Now.AddMinutes(40);
            cookie.Domain = Request.RequestUri.Host;
            cookie.Path = "/";

            response.Headers.AddCookies(new CookieHeaderValue[] { cookie });

            return response;


        }
        [System.Web.Http.Route("price")]
        public IHttpActionResult GetCartPrice()
        {

            var id = GetCookieId();


            var cart = _cartRepository.Get(id);


            var products = _dataContext.GetCartProducts(cart);

            var price = _priceCalculator.ComputePrice(products);

            return Ok(price);
        }
       
    }




    public class CartAction
    {
        public string Command { get; set; }

        public string ProductId { get; set; }
    }
}
