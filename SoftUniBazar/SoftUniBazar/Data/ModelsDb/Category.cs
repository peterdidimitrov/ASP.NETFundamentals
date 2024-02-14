using System.ComponentModel.DataAnnotations;
using static SoftUniBazar.Constants.CategoryConstants;

namespace SoftUniBazar.Data.ModelsDb
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(CategoryNameMaxLength)]
        public string Name { get; set; } = string.Empty;

        public ICollection<Ad> Ads { get; set; } = new List<Ad>();
    }
}