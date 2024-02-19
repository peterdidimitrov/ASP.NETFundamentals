using static SeminarHub.Constants.SeminarConstants;

namespace SeminarHub.Models
{
    public class SeminarViewModel
    {
        public SeminarViewModel(int id, string topic, string lecture, DateTime startDate, string category, string organiser)
        {
            Id = id;
            Topic = topic;
            Lecturer = lecture;
            DateAndTime = startDate.ToString(SeminarDateFormat);
            Category = category;
            Organizer = organiser;

        }
        public int Id { get; set; }

        public string Topic { get; set; }

        public string Lecturer { get; set; }

        public string DateAndTime { get; set; }

        public string Category { get; set; }

        public string Organizer { get; set; }
    }
}
