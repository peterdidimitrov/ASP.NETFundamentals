using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovie.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [StringLength(Constants.MovieModelConstants.TitleMaxLength, MinimumLength = Constants.MovieModelConstants.TitleMinLength)]
        [Required]
        public string? Title { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:M/dd/yyyy}")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/dd/yyyy}")]
        public DateTime ReleaseDate { get; set; }

        [Range(Constants.MovieModelConstants.PriceMin, Constants.MovieModelConstants.PriceMax), DataType(DataType.Currency)]
        //[DisplayFormat(DataFormatString = "${0,0}")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [RegularExpression(Constants.MovieModelConstants.GerneRegularExpression)]
        [StringLength(Constants.MovieModelConstants.GerneMaxLength)]
        [Required]
        public string? Genre { get; set; }

        [RegularExpression(Constants.MovieModelConstants.RatingRegularExpression)]
        [StringLength(Constants.MovieModelConstants.RatingMaxLength)]
        public string? Rating { get; set; }
    }
}