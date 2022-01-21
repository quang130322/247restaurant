using Res247.Models.BaseEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Res247.Models.Common
{
    [Table("OrderShippers", Schema = "common")]
    public class OrderShipper : BaseEntity
    {
        [ForeignKey("Order")]
        public int OrderId { get; set; }

        public Order Order { get; set; }

        [ForeignKey("Shipper")]
        public int ShipperId { get; set; }

        public Shipper Shipper { get; set; }

        public int Status { get; set; }
    }
}
