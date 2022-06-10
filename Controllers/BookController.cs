namespace BooksStore.Controllers;

using BooksStore.Data;
using BooksStore.Models.Book;
using Microsoft.AspNetCore.Mvc;

public class BookController : Controller
{
    private readonly BooksStoreDbContext data;

    public BookController(BooksStoreDbContext data)
        => this.data = data;

    public IActionResult Add()
    {
        var authors = data.Authors
            .Select(a => new BookAuthorsModel
            {
                Id = a.Id,
                Name = a.Name
            })
            .ToList();

        var genres = data.Genres
            .Select(g => new BookGenresModel
            {
                Id = g.Id,
                Name = g.Name
            })
            .ToList();

        var bookData = new BookFormModel
        {
            Authors = authors,
            Genres = genres
        };

        return View(bookData);
    }

    [HttpPost]
    public IActionResult Add(BookFormModel book)
    {

        if (!ModelState.IsValid)
        {

            return View();
        }

        var authors = data.Authors
            .Select(a => new BookAuthorsModel
            {
                Id = a.Id,
                Name = a.Name
            })
            .ToList();

        var genres = data.Genres
            .Select(g => new BookGenresModel
            {
                Id = g.Id,
                Name = g.Name
            })
            .ToList();

        var bookData = new BookFormModel
        {
            Title = book.Title,
            Description = book.Description,
            ImageUrl = book.ImageUrl,
            YearPublished = book.YearPublished,
            Price = book.Price,
            Authors = authors,
            Genres = genres
        };

        return View(bookData);
    }
}
