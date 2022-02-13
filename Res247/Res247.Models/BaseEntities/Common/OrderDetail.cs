using Res247.Models.BaseEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Res247.Models.Common
{
    [Table("OrderDetails", Schema = "common")]
    public class OrderDetail : BaseEntity
    {
        [Required(ErrorMessage = "The {0} is required")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "The {0} is required")]
        public decimal Price { get; set; }

        [ForeignKey("Food")]
        public int FoodId { get; set; }

        public Food Food { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }

        public Order Order { get; set; }
    }
}
