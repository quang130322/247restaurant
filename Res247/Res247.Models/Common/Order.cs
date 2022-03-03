using Res247.Models.BaseEntities;
using Res247.Models.Security;
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

        public string OrderAddress { get; set; }

        public DateTime? OrderArrivedAt { get; set; }

        public string CancelReason { get; set; }

        [ForeignKey("Account")]
        public string AccountId { get; set; }

        public Account Account { get; set; }

        [ForeignKey("Shipper")]
        public int ShipperId { get; set; }

        public Shipper Shipper { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
