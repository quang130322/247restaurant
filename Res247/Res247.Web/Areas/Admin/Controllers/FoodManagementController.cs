using Res247.Models.Common;
using Res247.Services;
using Res247.Web.Areas.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Res247.Web.Areas.Admin.Controllers
{
    public class FoodManagementController : Controller
    {
        private readonly IFoodServices _foodServices;
        private readonly ICategoryService _categoryService;

        public FoodManagementController(IFoodServices foodServices, ICategoryService categoryService)
        {
            _foodServices = foodServices;
            _categoryService = categoryService;
        }

        // GET: Admin/FoodManagement
        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageIndex = 1, int pageSize = 10)
        {
            ViewData["CurrentPageSize"] = pageSize;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            Expression<Func<Food, bool>> filter = null;

            if (!string.IsNullOrEmpty(searchString))
            {
                filter = c => c.Name.Contains(searchString);
            }

            Func<IQueryable<Food>, IOrderedQueryable<Food>> orderBy = null;

            switch (sortOrder)
            {
                case "name_desc":
                    orderBy = n => n.OrderByDescending(c => c.Name);
                    break;
                case "Price":
                    orderBy = n => n.OrderBy(p => p.Price);
                    break;
                case "price_desc":
                    orderBy = n => n.OrderByDescending(p => p.Price);
                    break;
                default:
                    orderBy = n => n.OrderBy(p => p.Name);
                    break;
            }

            var foods = await _foodServices.GetAsync(filter: filter, orderBy: orderBy, pageIndex: pageIndex ?? 1, pageSize: pageSize);

            return View(foods);
        }

        // GET: Admin/FoodManagement/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(_categoryService.GetAll(), "Id", "Name");
            var foodViewModel = new FoodViewModel();
            return View(foodViewModel);
        }

        // POST: Admin/FoodManagement/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(FoodViewModel foodViewModel)
        {
            if (ModelState.IsValid)
            {
                var food = new Food
                {
                    Name = foodViewModel.Name,
                    Description = foodViewModel.Description,
                    Image = foodViewModel.Image,
                    Price = foodViewModel.Price,
                    CategoryId = foodViewModel.CategoryId
                };

                var result = _foodServices.Add(food);
                if (result > 0)
                {
                    TempData["Message"] = "Created successful!";
                }
                else
                {
                    TempData["Message"] = "Create failed!";
                }
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(_categoryService.GetAll(), "Id", "Name", foodViewModel.CategoryId);
            return View(foodViewModel);
        }

        // GET: Admin/FoodManagement/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var food = _foodServices.GetById((int)id);

            if (food == null)
            {
                return HttpNotFound();
            }

            var foodViewModel = new FoodViewModel()
            {
                Id = food.Id,
                Name = food.Name,
                Description = food.Description,
                Image = food.Image,
                Price = food.Price,
                CategoryId = food.CategoryId
            };

            ViewBag.CategoryId = new SelectList(_categoryService.GetAll(), "Id", "Name", foodViewModel.CategoryId);
            return View(foodViewModel);
        }

        // POST: Admin/FoodManagement/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(FoodViewModel foodViewModel)
        {
            if (ModelState.IsValid)
            {
                var food = _foodServices.GetById(foodViewModel.Id);
                if (food == null)
                {
                    return HttpNotFound();
                }

                food.Name = foodViewModel.Name;
                food.Description = foodViewModel.Description;
                food.Image = foodViewModel.Image;
                food.Price = foodViewModel.Price;
                food.CategoryId = foodViewModel.CategoryId;

                var result = _foodServices.Update(food);
                if (result)
                {
                    TempData["Message"] = "Update successful!";
                }
                else
                {
                    TempData["Message"] = "Update failed!";
                }
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(_categoryService.GetAll(), "Id", "Name", foodViewModel.CategoryId);
            return View(foodViewModel);
        }

        // POST: Admin/FoodManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var result = _foodServices.Delete(id);
            if (result)
            {
                TempData["Message"] = "Delete successful";
            }
            else
            {
                TempData["Message"] = "Delete successful";
            }
            return RedirectToAction("Index");
        }
    }
}
