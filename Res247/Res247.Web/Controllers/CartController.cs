using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Res247.Web.ViewModels;
using Res247.Services;
using Res247.Models.Common;

namespace Res247.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IFoodServices _foodServices;

        public CartController(IFoodServices foodServices)
        {
            _foodServices = foodServices;
        }

        // GET: Cart/Index
        public ActionResult Index()
        {
            var cart = Session["cart"] as List<CartItemViewModel> ?? new List<CartItemViewModel>();

            if (cart.Count == 0 || Session["cart"] == null)
            {
                ViewBag.Message = "Chưa có sản phẩm nào trong giỏ hàng.";
                return View();
            }

            decimal totalPrice = 0m;

            foreach (var item in cart)
            {
                totalPrice += item.Price * item.Quantity;
            }

            ViewBag.TotalPrice = totalPrice;

            return View(cart);
        }

        //GET: Cart/AddToCart/id
        public ActionResult AddToCart(int foodId)
        {
            var food = _foodServices.GetById(foodId);
            if (food == null)
            {
                return HttpNotFound();
            }
            List<CartItemViewModel> cart = (List<CartItemViewModel>)Session["cart"];
            if (cart == null)
            {
                cart = new List<CartItemViewModel>();
            }
            foreach(var cartItem in cart)
            {
                if(cartItem.Food.Id == foodId)
                {
                    cartItem.Quantity++;
                    Session["cart"] = cart;
                    return RedirectToAction("Index");
                }
            }
            CartItemViewModel item = new CartItemViewModel()
            {
                Food = food,
                Quantity = 1,
                Price = food.Price
            };
            cart.Add(item);

            Session["cart"] = cart;
            return RedirectToAction("Index");
        }

        public ActionResult RemoveItem(int itemIndex)
        {
            List<CartItemViewModel> cart = (List<CartItemViewModel>)Session["cart"];
            cart.RemoveAt(itemIndex);
            Session["cart"] = cart;
            return RedirectToAction("Index");
        }
    }
}