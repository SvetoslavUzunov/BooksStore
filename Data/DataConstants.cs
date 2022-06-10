namespace BooksStore.Data;

public class DataConstants
{
    public class Book
    {
        public const int TitleMinLength = 1; 
        public const int TitleMaxLength = 100;
        public const int DescriptionMinLength = 10;
        public const int DescriptionMaxLength = 10000;
        public const int PublishedYearMinValue = 0;
        public const int PublishedYearMaxValue = 2050;
        public const int PriceMinValue = 0;
        public const int PriceMaxValue = int.MaxValue;
    }

    public class Author
    {
        public const int NameMaxLength = 30;
    }

    public class Genre
    {
        public const int NameMaxLength = 30;
    }
}
