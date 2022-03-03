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

        public decimal TotalPrice { get; set; }

        public string OrderAddress { get; set; }

        [MaxLength(500, ErrorMessage = "The {0} must less than {1} characters")]
        public string Comment { get; set; }

        [Required(ErrorMessage = "The {0} is required")]
        public bool HealthStatus { get; set; }

        [Required(ErrorMessage = "The {0} is required")]
        public int Vaccination { get; set; }

        [Required(ErrorMessage = "The {0} is required")]
        public bool TravelToOtherPlace { get; set; }

        [Required(ErrorMessage = "The {0} is required")]
        public bool HaveSymptoms { get; set; }

        [Required(ErrorMessage = "The {0} is required")]
        public bool MeetCovidPatients { get; set; }
    }
}