using Res247.Models.BaseEntities;
using Res247.Models.Security;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Res247.Models.Common
{
    [Table("Shippers", Schema = "common")]
    public class Shipper : BaseEntity
    {
        public string Name { get; set; }

        public int Status { get; set; }

        public virtual ICollection<CovidShipper> CovidShippers { get; set; }
    }
}
