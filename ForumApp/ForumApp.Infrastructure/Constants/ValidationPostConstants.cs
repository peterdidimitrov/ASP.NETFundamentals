namespace ForumApp.Infrastructure.Constants
{
    /// <summary>
    /// Constants values used for model validation
    /// </summary>
    public static class ValidationPostConstants
    {
        /// <summary>
        /// Maximal Post Title length
        /// </summary>
        public const int PostTitleMaxLength = 50;

        /// <summary>
        /// Minimal Post Title length
        /// </summary>
        public const int PostTitleMinLength = 10;

        /// <summary>
        /// Maximal Post Content length
        /// </summary>
        public const int PostContentMaxLength = 1500;

        /// <summary>
        /// Minimal Post Content length
        /// </summary>
        public const int PostContentMinLength = 30;

        /// <summary>
        /// Require Error message text
        /// </summary>
        public const string RequireErrorMessage = "The {0} field is required.";
        
        /// <summary>
        /// Title Error message text
        /// </summary>
        public const string StringLengthErrorMessage = "The {0} must between {2} and {1} cheracters long.";
    }
}
