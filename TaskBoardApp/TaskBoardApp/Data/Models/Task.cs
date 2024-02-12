using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskBoardApp.Data.DataConstants;

namespace TaskBoardApp.Data.Models
{
    public class Task
    {
        [Key]
        [Comment("Task's Identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(TaskConstants.TaskTitleMaxLength)]
        [Comment("Task's Title")]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(TaskConstants.TaskDescriptionMaxLength)]
        [Comment("Task's Description")]
        public string Description { get; set; } = null!;

        [Comment("Time when task is created")]
        public DateTime CreatedOn { get; set; }

        [Comment("Board's Identifier")]
        public int BoardId { get; set; }

        [ForeignKey(nameof(BoardId))]
        public Board? Board { get; set; }
        
        [Required]
        [Comment("Owner's Identifier")]
        public string OwnerId { get; set; } = null!;

        [ForeignKey(nameof(OwnerId))]
        public IdentityUser Owner { get; set; } = null!;
    }
}
