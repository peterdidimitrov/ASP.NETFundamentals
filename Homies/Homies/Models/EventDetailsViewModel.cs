namespace Homies.Models
{
    public class EventDetailsViewModel
    {
        public EventDetailsViewModel(int id, string name, string description, string startingTime, string endTime, string type, string createdOn, string organiser)
        {
            Id = id;
            Name = name;
            Description = description;
            Start = startingTime;
            End = endTime;
            Organiser = organiser;
            CreatedOn = createdOn;
            Type = type;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Start { get; set; }

        public string End { get; set; }

        public string Organiser { get; set; }

        public string CreatedOn { get; set; }

        public string Type { get; set; }
    }
}
