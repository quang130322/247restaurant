using Res247.Models.BaseEntities;
using Res247.Models.Security;
using System.ComponentModel.DataAnnotations.Schema;

namespace Res247.Models.Common
{
    [Table("Shippers", Schema = "common")]
    public class Shipper : BaseEntity
    {
        public bool Status { get; set; }

        public virtual Account Account { get; set; }
    }
}
