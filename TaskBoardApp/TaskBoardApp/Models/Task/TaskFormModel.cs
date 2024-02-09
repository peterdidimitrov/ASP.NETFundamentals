using System.ComponentModel.DataAnnotations;
using TaskBoardApp.Data.DataConstants;

namespace TaskBoardApp.Models.Task
{
    public class TaskFormModel
    {
        [Required]
        [StringLength(TaskConstants.TaskTitleMaxLength, MinimumLength = TaskConstants.TaskTitleMinLength, ErrorMessage = TaskConstants.ErrorMasageTitleLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(TaskConstants.TaskDescriptionMaxLength, MinimumLength = TaskConstants.TaskDescriptionMinLength, ErrorMessage = TaskConstants.ErrorMasageDescriptionLength)]
        public string Description { get; set; } = null!;

        [Display(Name = "Board")]
        public int BoardId { get; set; }
        public IEnumerable<TaskBoardModel> Boards { get; set; } = null!;
    }
}
