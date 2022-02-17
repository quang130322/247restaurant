using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Res247.Web.ViewModels
{
    public class CartItemViewModel
    {
        public FoodViewModel Food { get; set; }

        public int Quantity { get; set; }
    }
}