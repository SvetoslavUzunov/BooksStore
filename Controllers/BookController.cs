namespace BooksStore.Controllers;

using BooksStore.Data;
using BooksStore.Data.Models;
using BooksStore.Models.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static GlobalConstants;

public class BookController : Controller
{
    private readonly BooksStoreDbContext data;

    public BookController(BooksStoreDbContext data)
        => this.data = data;

    [Authorize]
    public IActionResult Add()
    {
        var bookData = new BookFormModel
        {
            Authors = GetBookAuthors(),
            Genres = GetBookGenres()
        };

        return View(bookData);
    }

    [Authorize]
    [HttpPost]
    public IActionResult Add(BookFormModel book)
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

        TempData[GlobalMessageKey] = "Book was added!";

        return RedirectToAction(nameof(All));
    }

    public IActionResult All()
    {
        var books = data.Books
            .Select(b => new BookViewModel
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                ImageUrl = b.ImageUrl,
                YearPublished = b.YearPublished,
                Price = b.Price,
                AuthorId = b.AuthorId,
                AuthorName = b.Author.Name,
                GenreId = b.GenreId
            })
            .ToList();

        return View(books);
    }

    public IActionResult ByGenre(string name)
    {
        var books = data.Books
            .Select(b => new BookViewModel
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                ImageUrl = b.ImageUrl,
                YearPublished = b.YearPublished,
                Price = b.Price,
                AuthorId = b.AuthorId,
                GenreId = b.GenreId,
                GenreName = b.Genre.Name
            })
            .Where(b => b.GenreName == name)
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
        var bookData = data.Books.Select(b => new BookFormModel
        {
            Id = b.Id,
            Title = b.Title,
            Description = b.Description,
            ImageUrl = b.ImageUrl,
            YearPublished = b.YearPublished,
            Price = b.Price,
            AuthorId = b.AuthorId,
            AuthorName = b.Author.Name,
            GenreId = b.GenreId,
            GenreName = b.Genre.Name

        })
        .FirstOrDefault(b => b.Id == id);

        bookData.Authors = GetBookAuthors();
        bookData.Genres = GetBookGenres();

        return View(bookData);
    }

    [Authorize]
    [HttpPost]
    public IActionResult Edit(int id, BookFormModel book)
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

        TempData[GlobalMessageKey] = "Book was edited!";

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

        TempData[GlobalMessageKey] = "Book was deleted!";

        return RedirectToAction(nameof(All));
    }

    [Authorize]
    public IActionResult AddToFavourites(int id)
    {
        var bookToFavourites = data.Books
            .Find(id);

        if (bookToFavourites != null)
        {
            var favourite = new Favourite
            {
                Books = new List<Book>
                {
                    bookToFavourites,
                },
            };

            bookToFavourites.FavouriteId = favourite.Id;

            data.Favourites.Add(favourite);
            data.SaveChanges();

            TempData[GlobalMessageKey] = "You successfully added book to favourites!";
        }

        return RedirectToAction(nameof(All));
    }

    [Authorize]
    public IActionResult RemoveFromFavourites(int id)
    {
        var favouriteBookToRemove = data.Favourites
            .Include(f => f.Books)
            .Where(f => f.Books.Any(b => b.Id == id))
            .FirstOrDefault();

        if (favouriteBookToRemove != null)
        {
            data.Favourites.Remove(favouriteBookToRemove);
            data.SaveChanges();

            TempData[GlobalMessageKey] = "Book was removed from favourites!";
        }

        return RedirectToAction(nameof(Favourites));
    }

    public IActionResult Favourites()
    {
        var books = data.Books
            .Where(b => b.FavouriteId != null)
            .Select(b => new BookViewModel
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                ImageUrl = b.ImageUrl,
                YearPublished = b.YearPublished,
                Price = b.Price,
                AuthorId = b.AuthorId,
                AuthorName = b.Author.Name,
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