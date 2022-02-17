using Res247.Models.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Res247.Models.Common
{
    [Table("ShipperOrders", Schema = "common")]
    public class ShipperOrder : BaseEntity
    {
        [Required(ErrorMessage = "The {0} is required")]
        public int Status { get; set; }

        [Required(ErrorMessage = "The {0} is required")]
        public DateTime OrderTimeReceived { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }

        public Order Order { get; set; }

        [ForeignKey("Shipper")]
        public int ShipperId { get; set; }

        public Shipper Shipper { get; set; }

    }
}
