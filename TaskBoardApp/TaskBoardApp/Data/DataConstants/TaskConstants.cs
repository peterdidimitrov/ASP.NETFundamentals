namespace TaskBoardApp.Data.DataConstants
{
    public static class TaskConstants
    {
        public const int TaskTitleMaxLength = 70;
        public const int TaskTitleMinLength = 5;
        public const string ErrorMasageTitleLength = "Title should be between {2} and {1} symbols.";

        public const int TaskDescriptionMaxLength = 1000;
        public const int TaskDescriptionMinLength = 10;
        public const string ErrorMasageDescriptionLength = "Description should be between {2} and {1} symbols.";
    }
}
