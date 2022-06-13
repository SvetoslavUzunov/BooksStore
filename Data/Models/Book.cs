namespace BooksStore.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static DataConstants.Book;

public class Book
{
    public int Id { get; init; }

    [Required]
    [MaxLength(TitleMaxLength)]
    public string Title { get; set; }

    [Required]
    [MaxLength(DescriptionMaxLength)]
    public string Description { get; set; }

    [Required]
    public string ImageUrl { get; set; }

    [Required]
    public int YearPublished { get; set; }

    [Required]
    public decimal Price { get; set; }

    public int AuthorId { get; set; }

    public Author Author { get; set; }

    public int GenreId { get; set; }

    public Genre Genre { get; set; }

    public int? FavouriteId { get; set; }
    public Favourite? Favourite { get; set; }
}
