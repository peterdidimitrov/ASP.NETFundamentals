using System.ComponentModel.DataAnnotations;
using static SoftUniBazar.Constants.AdConstants;
using static SoftUniBazar.Constants.ErrorConstants;

namespace SoftUniBazar.Models
{
    public class AdFormViewModel
    {
        [Required(ErrorMessage = RequireError)]
        [StringLength(AdNameMaxLength, MinimumLength = AdNameMinLength, ErrorMessage = ErrorMasageLength)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = RequireError)]
        [StringLength(AdDescriptionMaxLength, MinimumLength = AdDescriptionMinLength, ErrorMessage = ErrorMasageLength)]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = RequireError)]
        public string ImageUrl { get; set; } = string.Empty;

        public decimal Price { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
    }
}
