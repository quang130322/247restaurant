using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Res247.Web.ViewModels
{
    public class FoodViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The {0} is required")]
        [StringLength(255, ErrorMessage = "The {0} must between {2} and {1} characters", MinimumLength = 3)]
        public string Name { get; set; }

        [MaxLength(500, ErrorMessage = "The {0} must less than {1} characters")]
        public string Description { get; set; }

        public string Image { get; set; }

        [Required(ErrorMessage = "The {0} is required")]
        public decimal Price { get; set; }

        public IEnumerable<int> SelectedCategoryIds { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}