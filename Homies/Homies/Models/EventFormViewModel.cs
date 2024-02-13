using System.ComponentModel.DataAnnotations;
using static Homies.Constants.EventConstants;

namespace Homies.Models
{
    public class EventFormViewModel
    {
        [Required(ErrorMessage = RequireError)]
        [StringLength(EventNameMaxLength, MinimumLength = EventNameMinLength, ErrorMessage = ErrorMasageLength)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = RequireError)]
        [StringLength(EventDescriptionMaxLength, MinimumLength = EventDescriptionMinLength, ErrorMessage = ErrorMasageLength)]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = RequireError)]
        public string Start { get; set; } = string.Empty;

        [Required(ErrorMessage = RequireError)]
        public string End { get; set; } = string.Empty;

        [Required(ErrorMessage = RequireError)]
        [Display(Name = "Type of event")]
        public int TypeId { get; set; }

        public IEnumerable<TypeViewModel> Types { get; set; } = new List<TypeViewModel>();
    }
}
