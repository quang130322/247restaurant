<<<<<<< HEAD
﻿using Res247.Services;
using Res247.Web.ViewModel;
using System.Collections.Generic;
using System.Web.Mvc;
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Res247.Web.ViewModels;
using Res247.Services;
using Res247.Models.Common;
>>>>>>> d56a301ef03e1f4789bbbbae208cefd9bba6909e

namespace Res247.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IFoodServices _foodServices;

        public CartController(IFoodServices foodServices)
        {
            _foodServices = foodServices;
        }
<<<<<<< HEAD

=======
>>>>>>> d56a301ef03e1f4789bbbbae208cefd9bba6909e
        // GET: Cart/Index
        public ActionResult Index()
        {
            List<CartItemViewModel> cart = (List<CartItemViewModel>)Session["cart"];
            if (cart == null)
            {
                cart = new List<CartItemViewModel>();
            }
            return View(cart);
        }
<<<<<<< HEAD

=======
>>>>>>> d56a301ef03e1f4789bbbbae208cefd9bba6909e
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
<<<<<<< HEAD
            foreach (var cartItem in cart)
            {
                if (cartItem.Food.Id == foodId)
=======
            foreach(var cartItem in cart)
            {
                if(cartItem.Food.Id == foodId)
>>>>>>> d56a301ef03e1f4789bbbbae208cefd9bba6909e
                {
                    cartItem.Quantity++;
                    Session["cart"] = cart;
                    return RedirectToAction("Index");
                }
            }
            FoodViewModel foodViewModel = new FoodViewModel()
            {
                Id = food.Id,
                Name = food.Name,
                Description = food.Description,
                Image = food.Image,
                Price = food.Price
            };
            CartItemViewModel item = new CartItemViewModel()
            {
                Food = foodViewModel,
                Quantity = 1
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