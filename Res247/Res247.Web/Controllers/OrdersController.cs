using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Res247.Data;
using Res247.Models.Common;
using Res247.Services;
using Res247.Web.ViewModels;

namespace Res247.Web.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrderServices _orderServices;
        private readonly ICheckoutService _checkoutService;
        private readonly ICovidInfoServices _covidInfoServices;
        private readonly IOrderDetailServices _orderDetailServices;
        private readonly IFoodServices _foodServices;
        private readonly IShipperService _shipperService;
        private readonly ICovidShipperService _covidShipperService;

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

        public OrdersController(IOrderServices orderServices
            , ICheckoutService checkoutService
            , ICovidInfoServices covidInfoServices
            , IOrderDetailServices orderDetailServices
            , IFoodServices foodServices
            , IShipperService shipperService
            , ICovidShipperService covidShipperService)
        {
            _orderServices = orderServices;
            _checkoutService = checkoutService;
            _covidInfoServices = covidInfoServices;
            _orderDetailServices = orderDetailServices;
            _foodServices = foodServices;
            _shipperService = shipperService;
            _covidShipperService = covidShipperService;
        }

        public ActionResult Checkout()
        {
            if (Request.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var user = UserManager.FindById(userId);
                var covid = _covidInfoServices.GetCovidInfoByAccountId(userId);

                var orderViewModel = new OrderViewModel
                {
                    CusName = user.Name,
                    OrderAddress = user.Address,
                    PhoneNumber = user.PhoneNumber,
                    Vaccination = covid.Vaccination,
                    HealthStatus = covid.HealthStatus,
                    HaveSymptoms = covid.HaveSymptoms,
                    MeetCovidPatients = covid.MeetCovidPatients,
                    TravelToOtherPlace = covid.TravelToOtherPlace
                };

                return View(orderViewModel);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Checkout(OrderViewModel orderViewModel)
        {
            var cart = Session["cart"];
            var cartItems = (List<CartItemViewModel>)cart;

            if (cartItems.Count == 0)
            {
                return View("Index", "Cart");
            }

            decimal TotalPrice = 0;

            if (ModelState.IsValid) {
                var orderDetails = new List<OrderDetail>();
                foreach (var item in cartItems)
                {
                    var orderDetail = new OrderDetail
                    {
                        Quantity = item.Quantity,
                        FoodId = item.Food.Id,
                        Price = item.Price * item.Quantity
                    };
                    TotalPrice += item.Price * item.Quantity;
                    orderDetails.Add(orderDetail);
                }

                var order = new Order
                {
                    Comment = orderViewModel.Comment,
                    AccountId = User.Identity.GetUserId(),
                    TotalPrice = TotalPrice,
                    OrderAddress = orderViewModel.OrderAddress,
                    Status = 0,
                    ShipperId = 1
                };

                _checkoutService.Checkout(order, orderDetails);

                cartItems.Clear();
                return RedirectToAction("CheckoutSuccess");
            }
            return View(orderViewModel);
        }

        public void UpdateUserInfomation(OrderViewModel model)
        {
            var userId = User.Identity.GetUserId();
            var user = UserManager.FindById(userId);
            var covid = _covidInfoServices.GetCovidInfoByAccountId(userId);

            user.Name = model.CusName;
            user.Address = model.OrderAddress;
            user.PhoneNumber = model.PhoneNumber;
            covid.HealthStatus = model.HealthStatus;
            covid.Vaccination = model.Vaccination;
            covid.HaveSymptoms = model.HaveSymptoms;
            covid.TravelToOtherPlace = model.TravelToOtherPlace;
            covid.MeetCovidPatients = model.MeetCovidPatients;

            UserManager.Update(user);
            _covidInfoServices.Add(covid);
        }

        public ActionResult OrderHistory()
        {
            var userId = User.Identity.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return HttpNotFound();
            }
            var orders = _orderServices.GetOrderHistory(userId);
            return View(orders);
        }

        public ActionResult Details(int id)
        {
            var order = _orderServices.GetById(id);
            var user = UserManager.FindById(order.AccountId);

            var orderDetailViewModel = new OrderDetailViewModel
            {
                Id = order.Id,
                OrderAddress = order.OrderAddress,
                Comment = order.Comment,
                CusName = user.Name,
                Status = order.Status,
                OrderDate = order.OrderDate,
                OrderArrivedAt = order.OrderArrivedAt,
                PhoneNumber = user.PhoneNumber,
                TotalPrice = order.TotalPrice,
                CancelReason = order.CancelReason,
                ShipperId = order.ShipperId
            };
            return View(orderDetailViewModel);
        }

        [HttpPost]
        public ActionResult Details(OrderDetailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var order = _orderServices.GetById(model.Id);

                order.Status = -1;
                order.CancelReason = model.CancelReason;

                var result = _orderServices.Update(order);
                if (result)
                {
                    TempData["Message"] = "Hủy đơn hàng thành công!";
                }
                else
                {
                    TempData["Message"] = "Hủy đơn hàng thất bại!";
                }
                return RedirectToAction("OrderHistory");
            }
            return View(model);
        }

        public ActionResult ShipperInfo(int shipperId)
        {
            var shipper = _shipperService.GetById(shipperId);
            var covid = _covidShipperService.GetCovidShipperByShipperId(shipperId);
            var shipperViewModel = new ShipperViewModel
            {
                HaveSymptoms = covid.HaveSymptoms,
                HealthStatus = covid.HealthStatus,
                MeetCovidPatients = covid.MeetCovidPatients,
                TravelToOtherPlace = covid.TravelToOtherPlace,
                Name = shipper.Name,
                Vaccination = covid.Vaccination
            };
            return PartialView("_ShipperInfo", shipperViewModel);
        }

        public ActionResult GetOrderDetails(int orderId)
        {
            var list = _orderDetailServices.GetOrderDetailsByOrder(orderId);
            return PartialView("_GetOrderDetails", list);
        }

        public string GetFoodName(int foodId)
        {
            var name = _foodServices.GetById(foodId).Name;
            return name;
        }

        public ActionResult CheckoutSuccess()
        {
            var userId = User.Identity.GetUserId();
            var user = UserManager.FindById(userId);
            ViewBag.CheckoutSuccess = user.Name + ", cảm ơn bạn đã đặt hàng, đơn hàng của bạn sẽ được ship nhanh nhất.";
            return View();
        }
    }
}
