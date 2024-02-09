using System.ComponentModel.DataAnnotations;
using TaskBoardApp.Data.DataConstants;

namespace TaskBoardApp.Data.Models
{
    public class Board
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(BoardConstants.BoardNameMaxLength)]
        public string Name { get; set; } = null!;

        public IEnumerable<Task> Tasks { get; set; } = new HashSet<Task>();
    }
}