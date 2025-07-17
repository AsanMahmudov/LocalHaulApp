namespace LocalHaul.GlobalCommon
{
    public static class ValidationConstants
    {
        /// <summary>
        /// Defines validation constants for the Product entity.
        /// </summary>
        public static class Product
        {
            public const int TitleMinLength = 5;
            public const int TitleMaxLength = 100;

            public const int DescriptionMinLength = 10;
            public const int DescriptionMaxLength = 1000;

            public const double PriceMinValue = 0.01;
            public const double PriceMaxValue = 1000000.00;

            public const string PriceColumnType = "decimal(18, 2)";
        }

        /// <summary>
        /// Defines validation constants for the Category entity.
        /// </summary>
        public static class Category
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 50;
        }

        /// <summary>
        /// Defines validation constants for the ApplicationUser entity.
        /// </summary>
        public static class ApplicationUser
        {
            public const int FirstNameMaxLength = 50;
            public const int LastNameMaxLength = 50;
            public const int CityMaxLength = 100;
        }

        /// <summary>
        /// Defines validation constants for the Image entity.
        /// </summary>
        public static class Image
        {
            public const int ImagePathMaxLength = 255;
        }

        /// <summary>
        /// Defines validation constants for the Message entity.
        /// </summary>
        public static class Message
        {
            public const int ContentMinLength = 1;
            public const int ContentMaxLength = 500;
        }
    }
}
