using Res247.Models.BaseEntities;
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

        public virtual Customer Customer { get; set; }
        
        public virtual Shipper Shipper { get; set; }
    }
}
