using static ForumApp.Infrastructure.Constants.ValidationPostConstants;
using System.ComponentModel.DataAnnotations;


namespace ForumApp.Core.Models
{
    /// <summary>
    /// Post data transfer model
    /// </summary>
    public class PostModel
    {
        /// <summary>
        /// Post identificator
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        /// Post Title
        /// </summary>
        [Required(ErrorMessage = RequireErrorMessage)]
        [StringLength(PostTitleMaxLength, MinimumLength = PostTitleMinLength, ErrorMessage = StringLengthErrorMessage)]
        public required string Title { get; set; }

        /// <summary>
        /// Post Content
        /// </summary>
        [Required(ErrorMessage = RequireErrorMessage)]
        [StringLength(PostContentMaxLength, MinimumLength = PostContentMinLength, ErrorMessage = StringLengthErrorMessage)]
        public required string Content { get; set; }
    }
}
