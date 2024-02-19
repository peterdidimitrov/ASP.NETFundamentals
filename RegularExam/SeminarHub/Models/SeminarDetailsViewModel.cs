namespace SeminarHub.Models
{
    public class SeminarDetailsViewModel
    {
        public SeminarDetailsViewModel(int id, string topic, string lecturer, string details, string date, string duration, string category, string organizer)
        {
            Id = id;
            Topic = topic;
            Lecturer = lecturer;
            Details = details;
            DateAndTime = date;
            Duration = duration;
            Category = category;
            Organizer = organizer;
        }

        public int Id { get; set; }

        public string Topic { get; set; }

        public string Lecturer { get; set; }

        public string Details { get; set; }

        public string DateAndTime { get; set; }

        public string Duration { get; set; }

        public string Category { get; set; }

        public string Organizer { get; set; }
    }
}
