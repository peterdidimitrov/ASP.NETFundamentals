using static ForumApp.Infrastructure.Constants.ValidationPostConstants;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ForumApp.Infrastructure.Data.Models
{
    [Comment("Post table")]
    public class Post
    {
        [Key]
        [Comment("Post identifier")]
        public int Id { get; init; }

        [Required]
        [Comment("Post title")]
        [MaxLength(PostTitleMaxLength)]
        public required string Title { get; set; }

        [Required]
        [Comment("Post content")]
        [MaxLength(PostContentMaxLength)]
        public required string Content { get; set; }
    }
}
//•	Title – a string with min length 10 and max length 50 (required)
//•	Content – a string with min length 30 and max length 1500 (required)
