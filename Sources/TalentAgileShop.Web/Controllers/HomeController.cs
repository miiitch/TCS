using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TalentAgileShop.Web.Controllers
{
    [RoutePrefix("")]
    public class HomeController : Controller
    {
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("catalog")]
        public ActionResult Catalog()
        {
            ViewBag.Message = "Product catalog";

            return View();
        }

        [Route("cart")]
        public ActionResult Cart()
        {
            ViewBag.Message = "Cart";

            return View();
        }
    }
}