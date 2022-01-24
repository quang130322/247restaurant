using Res247.Models.BaseEntities;
using Res247.Models.Security;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Res247.Models.Common
{
    [Table("CovidInfo", Schema = "common")]
    public class CovidInfo : BaseEntity
    {
        [Required(ErrorMessage = "The {0} is required")]
        public int Vaccination { get; set; }

        [Required(ErrorMessage = "The {0} is required")]
        public bool HealthStatus { get; set; }

        public DateTime DateCreated { get; set; } 

        [ForeignKey("Account")]
        public int AccountId { get; set; }

        public Account Account { get; set; }
    }
}
