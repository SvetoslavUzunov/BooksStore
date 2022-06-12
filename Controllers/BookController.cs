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
        var bookData = new AddBookFormModel
        {
            Authors = GetBookAuthors(),
            Genres = GetBookGenres()
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

        return RedirectToAction(nameof(All));
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
                GenreId = b.GenreId,
                AuthorName = b.Author.Name,
            })
            .ToList();

        return View(books);
    }

    public IActionResult Details(int id)
    {
        var book = data.Books
            .Select(b => new DetailsBookViewModel
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                ImageUrl = b.ImageUrl,
                YearPublished = b.YearPublished,
                Price = b.Price,
                AuthorName = b.Author.Name,
                GenreName = b.Genre.Name
            })
            .FirstOrDefault(b => b.Id == id);

        return View(book);
    }

    [Authorize]
    public IActionResult Edit(int id)
    {
        var bookData = data.Books.Select(b => new EditBookFormModel
        {
            Id = b.Id,
            Title = b.Title,
            Description = b.Description,
            ImageUrl = b.ImageUrl,
            YearPublished = b.YearPublished,
            Price = b.Price
        })
        .FirstOrDefault(b => b.Id == id);

        bookData.Authors = GetBookAuthors();
        bookData.Genres = GetBookGenres();

        return View(bookData);
    }

    [Authorize]
    [HttpPost]
    public IActionResult Edit(int id, EditBookFormModel book)
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

        var bookData = data.Books.Find(id);

        if (bookData != null)
        {
            bookData.Title = book.Title;
            bookData.Description = book.Description;
            bookData.ImageUrl = book.ImageUrl;
            bookData.YearPublished = book.YearPublished;
            bookData.Price = book.Price;
            bookData.AuthorId = book.AuthorId;
            bookData.GenreId = book.GenreId;

            data.SaveChanges();
        }

        return RedirectToAction(nameof(All));
    }

    [Authorize]
    public IActionResult Delete(int id)
    {
        var bookToDelete = data.Books.Find(id);

        if (bookToDelete != null)
        {
            data.Books.Remove(bookToDelete);
            data.SaveChanges();
        }

        return RedirectToAction(nameof(All));
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