using System.ComponentModel.DataAnnotations;
using TaskBoardApp.Data.DataConstants;

namespace TaskBoardApp.Models
{
    public class BoardViewModel
    {
        public int Id { get; init; }

        [StringLength(BoardConstants.BoardNameMaxLength, MinimumLength = BoardConstants.BoardNameMinLength, ErrorMessage = BoardConstants.ErrorMasageNameLength)]
        public string Name { get; init; } = null!;
        public IEnumerable<TaskViewModel> Tasks { get; set; } = new List<TaskViewModel>();
    }
}
