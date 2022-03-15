using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Res247.Web.ViewModels
{
    public class OrderDetailViewModel
    {
        public int Id { get; set; }

        public string CusName { get; set; }

        public string PhoneNumber { get; set; }

        public decimal TotalPrice { get; set; }

        public string OrderAddress { get; set; }

        public DateTime? OrderArrivedAt { get; set; }

        public int ShipperId { get; set; }

        public int Status { get; set; }

        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "The {0} is required")]
        public string CancelReason { get; set; }

        [MaxLength(500, ErrorMessage = "The {0} must less than {1} characters")]
        public string Comment { get; set; }
    }
}