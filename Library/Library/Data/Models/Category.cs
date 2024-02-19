using System.ComponentModel.DataAnnotations;
using static Library.Constants.CategoryConstants;

namespace Library.Data.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(CategoryNameMaxLength)]
        public string Name { get; set; } = string.Empty;

        public ICollection<Book> Books { get; set; } = new HashSet<Book>();
    }
}