namespace BooksStore.Data.Models;

public class Favourite
{
    public int Id { get; init; }

    public IEnumerable<Book> Books { get; set; } = new List<Book>();
}