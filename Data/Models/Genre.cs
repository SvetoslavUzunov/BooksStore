namespace BooksStore.Data.Models;

using System.ComponentModel.DataAnnotations;
using static DataConstants.Genre;

public class Genre
{
    public int Id { get; init; }

    [Required]
    [MaxLength(NameMaxLength)]
    public string Name { get; set; }

    public IEnumerable<Book> Books { get; set; } = new List<Book>();
}
