using Res247.Models.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Res247.Models.Common
{
    [Table("Orders", Schema = "common")]
    public class Order : BaseEntity
    {
        [Required(ErrorMessage = "The {0} is required")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "The {0} is required")]
        public decimal TotalPrice { get; set; }

        [MaxLength(500, ErrorMessage = "The {0} must less than {1} characters")]
        public string Comment { get; set; }

        [Required(ErrorMessage = "The {0} is required")]
        public int Status { get; set; }

        public bool IsPaid { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public virtual ICollection<ShipperOrder> ShipperOrders { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
