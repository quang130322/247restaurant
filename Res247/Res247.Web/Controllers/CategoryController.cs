using Res247.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Res247.Web.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryService _categoryServices;
        private IFoodServices _foodServices;

        public CategoryController(CategoryServices categoryServices, FoodServices foodServices)
        {
            _categoryServices = categoryServices;
            _foodServices = foodServices;
        }

        public ActionResult GetAllCategory()
        {
            var categories = _categoryServices.GetAll();
            return PartialView("_ListCategories", categories);
        }

        public ActionResult GetFoodsByCategory(int cateId)
        {
            var cate = _categoryServices.GetById(cateId);
            var foods = _foodServices.GetFoodsByCate(cate.Id);
            return PartialView("_ListFoods", foods);
        }
    }
}