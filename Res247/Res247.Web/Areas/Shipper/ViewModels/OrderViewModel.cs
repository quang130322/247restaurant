using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Res247.Web.Areas.Shipper.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The {0} is required")]
        public DateTime OrderDate { get; set; }

        public string CusName { get; set; }

        [Required(ErrorMessage = "The {0} is required")]
        public decimal TotalPrice { get; set; }
        public string OrderAddress { get; set; }

        [MaxLength(500, ErrorMessage = "The {0} must less than {1} characters")]
        public string Comment { get; set; }

        [Required(ErrorMessage = "The {0} is required")]
        public int Status { get; set; }

        [Required(ErrorMessage = "The {0} is required")]
        public string CancelReason { get; set; }

        public bool IsPaid { get; set; }

        public DateTime? OrderArrivedAt { get; set; }
    }
}