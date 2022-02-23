using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Res247.Web.ViewModels
{
    public class CovidInfoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The {0} is required")]
        public bool HealthStatus { get; set; }

        [Required(ErrorMessage = "The {0} is required")]
        public int Vaccination { get; set; }
    }
}