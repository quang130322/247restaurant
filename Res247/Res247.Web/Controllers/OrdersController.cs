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

        public OrdersController(IOrderServices orderServices, ICheckoutService checkoutService)
        {
            _orderServices = orderServices;
            _checkoutService = checkoutService;
        }

        public ActionResult Checkout()
        {
            if (Request.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var user = UserManager.FindById(userId);

                var orderViewModel = new OrderViewModel
                {
                    CusName = user.Name,
                    CusAddress = user.Address,
                    PhoneNumber = user.PhoneNumber
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

            if (ModelState.IsValid)
            {
                List<OrderDetail> orderDetails = new List<OrderDetail>();

                foreach (var item in cartItems)
                {
                    var orderDetail = new OrderDetail
                    {
                        Food = item.Food,
                        Quantity = item.Quantity,
                        Price = item.Price * item.Quantity
                    };
                    TotalPrice += item.Quantity * item.Price;
                    orderDetails.Add(orderDetail);
                }

                var order = new Order
                {
                    Comment = orderViewModel.Comment,
                    AccountId = User.Identity.GetUserId(),
                    TotalPrice = TotalPrice,
                    Status = 0,
                    ShipperId = 1
                };

                _checkoutService.Checkout(order, orderDetails);

                cartItems.Clear();
                return RedirectToAction("CheckoutSuccess");
            }
            return View(orderViewModel);
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
