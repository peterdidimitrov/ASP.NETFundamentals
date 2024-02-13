namespace Homies.Constants
{
    public static class EventConstants
    {
        public const int EventNameMaxLength = 20;
        public const int EventNameMinLength = 5;
        
        public const int EventDescriptionMaxLength = 150;
        public const int EventDescriptionMinLength = 15;

        public const string EventDateFormat = "yyyy-MM-dd H:mm";
        public const string RequireError = "The field {0} is required.";
        public const string ErrorMasageLength = "The field {0} must be between {2} and {1} characters long.";
    }
}
