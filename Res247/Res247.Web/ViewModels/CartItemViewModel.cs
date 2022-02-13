using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Res247.Web.ViewModels
{
    public class CartItemViewModel
    {
        public FoodViewModel food { get; set; }

        public int quantity { get; set; }
    }
}