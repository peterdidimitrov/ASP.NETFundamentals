namespace Homies.Models
{
    public class TypeViewModel
    {
        public TypeViewModel(string name) 
        {
            Name = name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
