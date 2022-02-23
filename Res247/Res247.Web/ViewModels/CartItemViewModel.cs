﻿using Res247.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Res247.Web.ViewModels
{
    public class CartItemViewModel
    {
        public Food Food { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}