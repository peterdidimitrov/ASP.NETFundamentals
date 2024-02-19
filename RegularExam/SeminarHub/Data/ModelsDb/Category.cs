using System.ComponentModel.DataAnnotations;
using static SeminarHub.Constants.CategoryConstants;

namespace SeminarHub.Data.ModelsDb
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(CategoryNameMaxLength)]
        public string Name { get; set; } = string.Empty;

        public ICollection<Seminar> Seminars { get; set; } = new HashSet<Seminar>();
    }
}