using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Res247.Data;
using Res247.Models.Common;
using Res247.Services;
using Res247.Web.Areas.Admin.ViewModels;

namespace Res247.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class CategoryManagementController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryManagementController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: Admin/CategoryManagement
        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageIndex = 1, int pageSize = 10)
        {
            ViewData["CurrentPageSize"] = pageSize;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            Expression<Func<Category, bool>> filter = null;

            if (!string.IsNullOrEmpty(searchString))
            {
                filter = c => c.Name.Contains(searchString);
            }

            Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null;

            switch (sortOrder)
            {
                case "name_desc":
                    orderBy = n => n.OrderByDescending(c => c.Name);
                    break;
                default:
                    orderBy = n => n.OrderBy(p => p.Name);
                    break;
            }

            var categories = await _categoryService.GetAsync(filter: filter, orderBy: orderBy, pageIndex: pageIndex ?? 1, pageSize: pageSize);

            return View(categories);
        }

        // GET: Admin/CategoryManagement/Create
        public ActionResult Create()
        {
            var categoryViewModel = new CategoryViewModel();
            return View(categoryViewModel);
        }

        // POST: Admin/CategoryManagement/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                var cate = new Category
                {
                    Name = categoryViewModel.Name,
                    Description = categoryViewModel.Description
                };
                var result = _categoryService.Add(cate);
                if (result > 0)
                {
                    TempData["Message"] = "Create successful";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Message"] = "Create failed! Please try again";
                }
                return RedirectToAction("Index");
            }

            return View(categoryViewModel);
        }

        // GET: Admin/CategoryManagement/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var cate = _categoryService.GetById((int)id);
            if(cate == null)
            {
                return HttpNotFound();
            }

            var categoryViewModel = new CategoryViewModel
            {
                Name = cate.Name,
                Description = cate.Description
            };

            return View(categoryViewModel);
        }

        // POST: Admin/CategoryManagement/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(CategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                var cate = _categoryService.GetById(categoryViewModel.Id);
                if(cate == null)
                {
                    return HttpNotFound();
                }

                cate.Name = categoryViewModel.Name;
                cate.Description = categoryViewModel.Description;

                var result = _categoryService.Update(cate);
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
            return View(categoryViewModel);
        }

        

        // POST: Admin/CategoryManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var result = _categoryService.Delete(id);
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
