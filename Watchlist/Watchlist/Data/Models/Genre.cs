using System.ComponentModel.DataAnnotations;
using static Watchlist.Constants.GenreConstants;

namespace Watchlist.Data.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(GenreNameMaxLength)]
        public string Name { get; set; } = string.Empty;
        public ICollection<Movie> Movies { get; set; } = new HashSet<Movie>();
    }
}
