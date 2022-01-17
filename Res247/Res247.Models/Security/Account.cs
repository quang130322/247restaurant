using Res247.Models.BaseEntities;
using Res247.Models.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Res247.Models.Security
{
    [Table("Accounts", Schema ="security")]
    public class Account : BaseEntity
    {
        [Required(ErrorMessage = "The {0} is required")]
        [StringLength(32, ErrorMessage = "The {0} must between {2} and {1} characters", MinimumLength = 5)]
        public string Username { get; set; }

        [Required(ErrorMessage = "The {0} is required")]
        [StringLength(32, ErrorMessage = "The {0} must between {2} and {1} characters", MinimumLength = 5)]
        public string Password { get; set; }

        [Required(ErrorMessage = "The {0} is required")]
        [StringLength(10, ErrorMessage = "The {0} must between {1} characters")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "The {0} is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The {0} is required")]
        [StringLength(255, ErrorMessage = "The {0} must between {2} and {1} characters", MinimumLength = 3)]
        public string Address { get; set; }

        [Required(ErrorMessage = "The {0} is required")]
        [StringLength(255, ErrorMessage = "The {0} must between {2} and {1} characters", MinimumLength = 2)]
        public string Name { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Shipper Shipper { get; set; }
    }
}
