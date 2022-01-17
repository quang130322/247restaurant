using Res247.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Res247.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFoodServices _foodServices;

        public HomeController(IFoodServices foodServices)
        {
            _foodServices = foodServices;
        }

        public ActionResult Index()
        {
            var foods = _foodServices.GetAll();
            return View(foods);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}