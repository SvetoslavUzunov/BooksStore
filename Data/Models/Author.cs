namespace BooksStore.Data.Models;

using System.ComponentModel.DataAnnotations;
using static DataConstants.Author; 

public class Author
{
    public int Id { get; init; }

    [Required]
    [MaxLength(NameMaxLength)]
    public string Name { get; set; }

    public IEnumerable<Book> Books { get; set; } = new List<Book>();
}
