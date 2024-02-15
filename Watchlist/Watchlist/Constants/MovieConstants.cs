namespace Watchlist.Constants
{
    public static class MovieConstants
    {
        public const int MovieTitleMaxLength = 50;
        public const int MovieTitleMínLength = 10;
        
        public const int MovieDirectorNameMaxLength = 50;
        public const int MovieDirectorNameMínLength = 5;

        public const double MovieRatingMaxLength = 10.00;
        public const double MovieRatingMinLength = 0.00;

        public const string MovieRatingErrorMessageRange = "The {0} must be between {1} and {2}.";
    }
}
