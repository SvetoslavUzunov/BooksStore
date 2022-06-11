namespace BooksStore.Models.Book;

using System.ComponentModel.DataAnnotations;
using static Data.DataConstants.Book;

public class AddBookFormModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
    public string Title { get; set; }

    [Required]
    [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
    public string Description { get; set; }

    [Display(Name = "Url")]
    [Required]
    [Url]
    public string ImageUrl { get; set; }

    [Display(Name = "Published")]
    [Range(PublishedYearMinValue, PublishedYearMaxValue)]
    public int YearPublished { get; set; }

    [Range(PriceMinValue, PriceMaxValue)]
    public decimal Price { get; set; }

    public int AuthorId { get; set; }
    public IEnumerable<BookAuthorsViewModel>? Authors { get; set; }

    public int GenreId { get; set; }
    public IEnumerable<BookGenresViewModel>? Genres { get; set; }
}