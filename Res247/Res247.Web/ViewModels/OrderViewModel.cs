using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Res247.Web.ViewModels
{
    public class OrderViewModel
    {
        public string CusName { get; set; }

        public string PhoneNumber { get; set; }

        public string CusAddress { get; set; }

        public decimal TotalPrice { get; set; }


        [MaxLength(500, ErrorMessage = "The {0} must less than {1} characters")]
        public string Comment { get; set; }
    }
}