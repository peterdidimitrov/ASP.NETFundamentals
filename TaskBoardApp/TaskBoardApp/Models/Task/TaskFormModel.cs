using System.ComponentModel.DataAnnotations;
using TaskBoardApp.Data.DataConstants;

namespace TaskBoardApp.Models.Task
{
    public class TaskFormModel
    {
        [Required(ErrorMessage = TaskConstants.RequireError)]
        [StringLength(TaskConstants.TaskTitleMaxLength, MinimumLength = TaskConstants.TaskTitleMinLength, ErrorMessage = TaskConstants.ErrorMasageLength)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = TaskConstants.RequireError)]
        [StringLength(TaskConstants.TaskDescriptionMaxLength, MinimumLength = TaskConstants.TaskDescriptionMinLength, ErrorMessage = TaskConstants.ErrorMasageLength)]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Board")]
        public int BoardId { get; set; }
        public IEnumerable<TaskBoardModel> Boards { get; set; } = new List<TaskBoardModel>();
    }
}
