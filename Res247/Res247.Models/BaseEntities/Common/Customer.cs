using Res247.Models.BaseEntities;
using Res247.Models.Security;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Res247.Models.Common
{
    [Table("Customers", Schema = "common")]
    public class Customer : BaseEntity
    {
        public virtual Account Account { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
