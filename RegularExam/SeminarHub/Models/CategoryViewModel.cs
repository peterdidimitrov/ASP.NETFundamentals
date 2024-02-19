namespace SeminarHub.Models
{
    public class CategoryViewModel
    {
        public CategoryViewModel(string name)
        {
            Name = name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
