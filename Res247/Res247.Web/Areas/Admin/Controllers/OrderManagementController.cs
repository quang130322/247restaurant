using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Res247.Models.Common;
using Res247.Services;
using Res247.Web.Areas.Admin.ViewModels;
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Res247.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class OrderManagementController : Controller
    {
        private readonly IOrderServices _orderServices;
        private readonly IFoodServices _foodServices;
        private readonly IOrderDetailServices _orderDetailServices;

        public OrderManagementController(IOrderServices orderServices, 
            IOrderDetailServices orderDetailServices,
            IFoodServices foodServices)
        {
            _orderServices = orderServices;
            _orderDetailServices = orderDetailServices;
            _foodServices = foodServices;
        }

        private AccountManager _userManager;
        public AccountManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<AccountManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Admin/OrderManagement
        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageIndex = 1, int pageSize = 10)
        {
            ViewData["CurrentPageSize"] = pageSize;
            ViewData["CurrentSort"] = sortOrder;

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            Expression<Func<Order, bool>> filter = null;

            if (!string.IsNullOrEmpty(searchString))
            {
                filter = c => c.OrderDate.ToString().Contains(searchString);
            }

            Func<IQueryable<Order>, IOrderedQueryable<Order>> orderBy = null;

            switch (sortOrder)
            {
                default:
                    orderBy = n => n.OrderByDescending(p => p.Id);
                    break;
            }

            var orders = await _orderServices.GetAsync(filter: filter, orderBy: orderBy, pageIndex: pageIndex ?? 1, pageSize: pageSize);

            return View(orders);
        }

        public ActionResult GetOrderDetails(int orderId)
        {
            var list = _orderDetailServices.GetOrderDetailsByOrder(orderId);
            return PartialView("GetOrderDetails",list);
        }

        public string GetFoodName(int foodId)
        {
            var name = _foodServices.GetById(foodId).Name;
            return name;
        }

        // GET: Admin/OrderManagement/Edit/5
        public ActionResult Edit(int? id)
        {
            var order = _orderServices.GetById((int)id);
            var orderViewModel = new OrderViewModel
            {
                Id = order.Id,
                Comment = order.Comment,
                OrderDate = order.OrderDate,
                OrderArrivedAt = order.OrderArrivedAt,
                OrderAddress = order.OrderAddress,
                IsPaid = order.IsPaid,
                Status = order.Status,
                TotalPrice = order.TotalPrice,
                CusName = UserManager.FindById(order.AccountId).Name
            };

            return View(orderViewModel);
        }

        // POST: Admin/OrderManagement/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var order = _orderServices.GetById(model.Id);
                if(order == null)
                {
                    return HttpNotFound();
                }

                order.Status = model.Status;
                order.OrderAddress = model.OrderAddress;

                if(order.Status > 1)
                {
                    order.OrderArrivedAt = DateTime.Now;
                    order.IsPaid = true;
                }

                var result = _orderServices.Update(order);
                if (result)
                {
                    TempData["Message"] = "Cập nhật thành công.";
                }
                else
                {
                    TempData["Message"] = "Cập nhật thất bại.";
                }

                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult NewOrder()
        {
            var orders = _orderServices.GetNewest();

            return View(orders);
        }

        public ActionResult GetShippingOrder()
        {
            var orders = _orderServices.GetShippingOrder();
            return View(orders);
        }
    }
}
