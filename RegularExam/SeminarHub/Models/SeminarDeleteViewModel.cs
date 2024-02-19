namespace SeminarHub.Models
{
    public class SeminarDeleteViewModel
    {
        public SeminarDeleteViewModel(int id, string topic, DateTime date)
        {
            Id = id;
            Topic = topic;
            DateAndTime = date;
        }
        public int Id { get; set; }

        public string Topic { get; set; }

        public DateTime DateAndTime { get; set; }
    }
}
