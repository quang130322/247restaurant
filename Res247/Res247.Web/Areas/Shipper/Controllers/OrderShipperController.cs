using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Res247.Data;
using Res247.Models.Common;
using Res247.Services;
using Res247.Web.Areas.Shipper.ViewModels;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Res247.Web.Areas.Shipper.Controllers
{
    [Authorize(Roles = "Administrator,Shipper")]
    public class OrderShipperController : Controller
    {
        private readonly IOrderServices _orderServices;
        private readonly IOrderDetailServices _orderDetailServices;
        private readonly IFoodServices _foodServices;
        private readonly IShipperService _shipperService;

        public OrderShipperController(IOrderDetailServices orderDetailServices,
            IOrderServices orderServices,
            IFoodServices foodServices,
            IShipperService shipperService)
        {
            _orderDetailServices = orderDetailServices;
            _orderServices = orderServices;
            _foodServices = foodServices;
            _shipperService = shipperService;
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

        // GET: Shipper/OrderShipper
        public ActionResult Index()
        {
            var shipperId = User.Identity.GetUserId();
            var newestOrder = _orderServices.GetNewest();
            var ongoingOrder = _orderServices.GetShippingOrderOfShipper(shipperId);
            var orders = newestOrder.Concat(ongoingOrder);
            return View(orders);
        }

        public ActionResult NewOrder()
        {
            var orders = _orderServices.GetNewest();

            return View(orders);
        }

        public ActionResult GetShippingOrder()
        {
            var shipperId = User.Identity.GetUserId();
            var orders = _orderServices.GetShippingOrderOfShipper(shipperId);
            return View(orders);
        }

        public ActionResult GetOrderDetails(int orderId)
        {
            var list = _orderDetailServices.GetOrderDetailsByOrder(orderId);
            return PartialView("GetOrderDetails", list);
        }

        public string GetFoodName(int foodId)
        {
            var name = _foodServices.GetById(foodId).Name;
            return name;
        }

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
                CusName = UserManager.FindById(order.AccountId).Name,
                CancelReason = order.CancelReason
            };

            return View(orderViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OrderViewModel model)
        {
            var accId = User.Identity.GetUserId();
            var shipper = CheckIfShipperExist(accId);

            if (ModelState.IsValid)
            {
                var order = _orderServices.GetById(model.Id);
                if (order == null)
                {
                    return HttpNotFound();
                }

                order.Status = model.Status;
                order.ShipperId = shipper.Id;

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

        public Models.Common.Shipper CheckIfShipperExist(string accId)
        {
            var user = UserManager.FindById(accId);
            var shipper = _shipperService.GetShipperByAccId(accId);
            if (shipper == null)
            {
                var newShip = new Models.Common.Shipper
                {
                    Account = user,
                    Status = true
                };
                _shipperService.Add(newShip);
                return newShip;
            }
            return shipper;
        }
    }
}
