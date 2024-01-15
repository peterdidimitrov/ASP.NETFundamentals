namespace MvcMovie.Constants
{
    public static class MovieModelConstants
    {
        //Title
        public const int TitleMinLength = 3;
        public const int TitleMaxLength = 60;

        //Price
        public const double PriceMin = 1;
        public const double PriceMax = 100;

        //Gerne
        public const string GerneRegularExpression = @"^[A-Z]+[a-zA-Z\s]*$";
        public const int GerneMinLength = 3;
        public const int GerneMaxLength = 30;

        //Rating
        public const string RatingRegularExpression = @"^[A-Z]+[a-zA-Z0-9""'\s-]*$";
        public const int RatingMinLength = 1;
        public const int RatingMaxLength = 5;
    }
}
