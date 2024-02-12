using Homies.Constants;
using System.ComponentModel.DataAnnotations;

namespace Homies.Data.ModelsDb
{
    public class Type
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(EventConstants.EventNameMaxLength)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(TypeConstants.TypeNameMaxLength)]
        
        public ICollection<Event> Events { get; set; } = new List<Event>();
    }
}
