using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using TalentAgileShop.Model;
using TalentAgileShop.Web.Models;

namespace TalentAgileShop.Web.Controllers
{
    [RoutePrefix("")]
    public class HomeController : Controller
    {
        private readonly FeatureSet _featureSet;
        private readonly IDataContext _dataContext;
        private readonly ICartRepository _cartRepository;
        private readonly ICartPriceCalculator _cartPriceCalculator;

        public HomeController(FeatureSet featureSet, IDataContext dataContext, ICartRepository cartRepository, ICartPriceCalculator cartPriceCalculator)
        {
            _featureSet = featureSet;
            _dataContext = dataContext;
            _cartRepository = cartRepository;
            _cartPriceCalculator = cartPriceCalculator;
        }


        [Route("")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("catalog")]
        public ActionResult Catalog()
        {
            var products = _dataContext.Products.Include(p => p.Category).Include(p => p.Origin).OrderBy(p => p.Name).ToList();


            var viewModel = new CatalogViewModel(products);

            return View(viewModel);
            
        }

        [Route("products/{id}")]
        public ActionResult Product(string id)
        {
            var product =
               _dataContext.Products.Include(p => p.Category).Include(p => p.Origin).FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return HttpNotFound();
            }

            var viewModel = new ProductViewModel(product);


            return View(viewModel);
        }


        [Route("products/{id}/image")]
        public ActionResult ProductImage(string id)
        {
            var product =
                _dataContext.Products.Include(p => p.Image).FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return HttpNotFound();
            }

            return this.File(product.Image.Data, "image/png");
        }


        [Route("cart")]
        public ActionResult Cart()
        {
            var basket = GetBasket();

            var products = _dataContext.GetCartProducts(basket);

            var price = _cartPriceCalculator.ComputePrice(products);


            return View(new CartViewModel(products, price));
        }

        private Model.Cart GetBasket()
        {
            var cookie = Request.Cookies.Get("cart-id");


            if (cookie == null)
            {
                return new Model.Cart();
            }

            var id = cookie.Value;

            if (id == null)
            {
                return new Model.Cart();
            }

            return _cartRepository.Get(id);
        }
    }
}