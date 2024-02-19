using System.ComponentModel.DataAnnotations;
using static SeminarHub.Constants.SeminarConstants;
using static SeminarHub.Constants.ErrorConstants;

namespace SeminarHub.Models
{
    public class SeminarFormViewModel
    {
        [Required(ErrorMessage = RequireError)]
        [StringLength(SeminarTopicMaxLength, MinimumLength = SeminarTopicMinLength, ErrorMessage = ErrorMasageLength)]
        [Display(Name = "Seminar topic")]
        public string Topic { get; set; } = string.Empty;

        [Required(ErrorMessage = RequireError)]
        [StringLength(SeminarLecturerMaxLength, MinimumLength = SeminarLecturerMinLength, ErrorMessage = ErrorMasageLength)]
        public string Lecturer { get; set; } = string.Empty;

        [Required(ErrorMessage = RequireError)]
        [StringLength(SeminarDetailsMaxLength, MinimumLength = SeminarDetailsMinLength, ErrorMessage = ErrorMasageLength)]
        [Display(Name = "More Info")]
        public string Details { get; set; } = string.Empty;

        [Required(ErrorMessage = RequireError)]
        [Display(Name = "Date of Seminar")]
        public string DateAndTime { get; set; } = string.Empty;

        [Required(ErrorMessage = RequireError)]
        [Range(30, 80, ErrorMessage = ErrorMessageRange)]
        public int Duration { get; set; }

        [Required(ErrorMessage = RequireError)]
        [Display(Name = "Type of event")]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; } = new HashSet<CategoryViewModel>();
    }
}
