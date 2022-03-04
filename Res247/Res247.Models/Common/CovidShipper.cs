using Res247.Models.BaseEntities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Res247.Models.Common
{
    public class CovidShipper:BaseEntity
    {
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

        public DateTime DateCreated { get; set; }

        [ForeignKey("Shipper")]
        public int ShipperId { get; set; }

        public Shipper Shipper { get; set; }
    }
}
