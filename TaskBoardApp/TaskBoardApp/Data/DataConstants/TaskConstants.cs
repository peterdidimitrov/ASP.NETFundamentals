namespace TaskBoardApp.Data.DataConstants
{
    public static class TaskConstants
    {
        public const int TaskTitleMaxLength = 70;
        public const int TaskTitleMinLength = 5;

        public const int TaskDescriptionMaxLength = 1000;
        public const int TaskDescriptionMinLength = 10;

        public const string ErrorMasageLength = "The field {0} must be between {2} and {1} characters long.";
        public const string RequireError = "The field {0} is required.";
    }
}
