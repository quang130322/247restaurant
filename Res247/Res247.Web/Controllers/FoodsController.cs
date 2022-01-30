using Res247.Services;
using System.Web.Mvc;

namespace Res247.Web.Controllers
{
    public class FoodsController : Controller
    {
        private IFoodServices _foodServices;

        public FoodsController(FoodServices foodServices)
        {
            _foodServices = foodServices;
        }


        public ActionResult Details(int foodId)
        {
            var food = _foodServices.GetById(foodId);
            if (food == null)
            {
                return HttpNotFound();
            }
            ViewBag.FoodName = food.Name;
            return View(food);
        }

        public ActionResult SimilarFood(int foodId)
        {
            var foods = _foodServices.GetFoodsByCate(foodId);
            return PartialView("_SimilarFood", foods);
        }
    }
}
