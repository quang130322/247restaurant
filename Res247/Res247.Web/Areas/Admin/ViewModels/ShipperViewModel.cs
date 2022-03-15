using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Res247.Web.Areas.Admin.ViewModels
{
    public class ShipperViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Status { get; set; }

        [Required(ErrorMessage = "The {0} is required")]
        public int Vaccination { get; set; }

        [Required(ErrorMessage = "The {0} is required")]
        public bool HealthStatus { get; set; }

        [Required(ErrorMessage = "The {0} is required")]
        public bool TravelToOtherPlace { get; set; }

        [Required(ErrorMessage = "The {0} is required")]
        public bool HaveSymptoms { get; set; }

        [Required(ErrorMessage = "The {0} is required")]
        public bool MeetCovidPatients { get; set; }
    }
}