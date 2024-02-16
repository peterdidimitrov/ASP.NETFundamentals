using System.ComponentModel.DataAnnotations;
using static Watchlist.Constants.ErrorConstants;
using static Watchlist.Constants.MovieConstants;

namespace Watchlist.Models
{
    public class MovieFormViewModel
    {
        [Required(ErrorMessage = ErrorMessageRequired)]
        [StringLength(MovieTitleMaxLength, MinimumLength = MovieTitleMínLength, ErrorMessage = ErrorMessageLength)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = ErrorMessageRequired)]
        [StringLength(MovieDirectorNameMaxLength, MinimumLength = MovieDirectorNameMínLength, ErrorMessage = ErrorMessageLength)]
        public string Director { get; set; } = string.Empty;

        [Required(ErrorMessage = ErrorMessageRequired)]
        public string ImageUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = ErrorMessageRequired)]
        [Range(MovieRatingMinLength, MovieRatingMaxLength, ErrorMessage = ErrorMessageRange)]
        public decimal Rating { get; set; }

        [Display(Name = "Genre")]
        public int GenreId { get; set; }

        public IEnumerable<GenreViewModel> Genres { get; set; } = new HashSet<GenreViewModel>();
    }
}
