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
            , ICovidInfoServices covidInfoServices)
        {
            _orderServices = orderServices;
            _checkoutService = checkoutService;
            _covidInfoServices = covidInfoServices;
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
                    HealthStatus = covid.HealthStatus
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

        public ActionResult CheckoutSuccess()
        {
            var userId = User.Identity.GetUserId();
            var user = UserManager.FindById(userId);
            ViewBag.CheckoutSuccess = user.Name + ", cảm ơn bạn đã đặt hàng, đơn hàng của bạn sẽ được ship nhanh nhất.";
            return View();
        }
    }
}
