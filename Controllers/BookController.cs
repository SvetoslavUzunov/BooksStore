namespace BooksStore.Controllers;

using BooksStore.Data;
using BooksStore.Data.Models;
using BooksStore.Models.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class BookController : Controller
{
    private readonly BooksStoreDbContext data;

    public BookController(BooksStoreDbContext data)
        => this.data = data;

    public IActionResult Add()
    {
        var authors = GetBookAuthors();
        var genres = GetBookGenres();

        var bookData = new AddBookFormModel
        {
            Authors = authors,
            Genres = genres
        };

        return View(bookData);
    }

    [Authorize]
    [HttpPost]
    public IActionResult Add(AddBookFormModel book)
    {
        if (!data.Authors.Any(a => a.Id == book.AuthorId))
        {
            ModelState.AddModelError(nameof(book.AuthorId), "Author does not exist!");
        }

        if (!data.Genres.Any(g => g.Id == book.GenreId))
        {
            ModelState.AddModelError(nameof(book.GenreId), "Genre does not exist!");
        }

        if (!ModelState.IsValid)
        {
            book.Authors = GetBookAuthors();
            book.Genres = GetBookGenres();

            return View(book);
        }

        var bookData = new Book
        {
            Title = book.Title,
            Description = book.Description,
            ImageUrl = book.ImageUrl,
            YearPublished = book.YearPublished,
            Price = book.Price,
            AuthorId = book.AuthorId,
            GenreId = book.GenreId
        };

        data.Books.Add(bookData);
        data.SaveChanges();

        return RedirectToAction("Index", "Home");
    }

    public IActionResult All()
    {
        var books = data.Books
            .Select(b => new AllBooksViewModel
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                ImageUrl = b.ImageUrl,
                YearPublished = b.YearPublished,
                Price = b.Price,
                AuthorId = b.AuthorId,
                GenreId = b.GenreId
            })
            .ToList();

        return View(books);
    }

    private IEnumerable<BookAuthorsViewModel> GetBookAuthors()
        => data.Authors
           .Select(a => new BookAuthorsViewModel
           {
               Id = a.Id,
               Name = a.Name
           })
           .ToList();

    private IEnumerable<BookGenresViewModel> GetBookGenres()
        => data.Genres
           .Select(g => new BookGenresViewModel
           {
               Id = g.Id,
               Name = g.Name
           })
           .ToList();
}