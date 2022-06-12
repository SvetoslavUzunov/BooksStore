namespace BooksStore.Models.Book;

public class DetailsBookViewModel
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string ImageUrl { get; set; }

    public int YearPublished { get; set; }

    public decimal Price { get; set; }

    public int AuthorId { get; set; }
    public string AuthorName { get; set; }
    public IEnumerable<BookAuthorsViewModel> Authors { get; set; }

    public int GenreId { get; set; }
    public string GenreName { get; set; }
    public IEnumerable<BookGenresViewModel> Genres { get; set; }
}
