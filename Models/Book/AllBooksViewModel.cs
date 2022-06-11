namespace BooksStore.Models.Book;

public class AllBooksViewModel
{
    public int Id { get; init; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string ImageUrl { get; set; }

    public int YearPublished { get; set; }

    public decimal Price { get; set; }

    public int AuthorId { get; set; }
    public IEnumerable<BookAuthorsViewModel> Authors { get; set; }

    public int GenreId { get; set; }
    public IEnumerable<BookGenresViewModel> Genres { get; set; }
}