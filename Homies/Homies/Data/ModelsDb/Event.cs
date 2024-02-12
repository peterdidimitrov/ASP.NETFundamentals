using Homies.Constants;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homies.Data.ModelsDb
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(EventConstants.EventNameMaxLength)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(EventConstants.EventDescriptionMaxLength)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string OrganiserId { get; set; } = string.Empty;
        
        [Required]
        public IdentityUser Organiser { get; set; } = null!;

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public DateTime Start { get; set; }
        
        [Required]
        public DateTime End { get; set; }

        [Required]
        public int TypeId { get; set; }

        [ForeignKey(nameof(TypeId))]
        public Type Type { get; set; } = null!;

        public ICollection<EventParticipant> EventsParticipants { get; set; } = new List<EventParticipant>();
    }
}
