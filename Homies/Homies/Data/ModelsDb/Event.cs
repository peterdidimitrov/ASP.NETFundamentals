using System.ComponentModel.DataAnnotations;
using Homies.Constants;

namespace Homies.Data.ModelsDb
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(EventConstants.EventNameMaxLength)]
        public string Name { get; set; } = string.Empty;
    }
}
//•	Has Description – a string with min length 15 and max length 150 (required)
//•	Has OrganiserId – an string (required)
//•	Has Organiser – an IdentityUser (required)
//•	Has CreatedOn – a DateTime with format "yyyy-MM-dd H:mm" (required) (the DateTime format is recommended, if you are having troubles with this one, you are free to use another one)
//•	Has Start – a DateTime with format "yyyy-MM-dd H:mm" (required) (the DateTime format is recommended, if you are having troubles with this one, you are free to use another one)
//•	Has End – a DateTime with format "yyyy-MM-dd H:mm" (required) (the DateTime format is recommended, if you are having troubles with this one, you are free to use another one)
//•	Has TypeId – an integer, foreign key (required)
//•	Has Type – a Type (required)
//•	Has EventsParticipants – a collection of type EventParticipant
