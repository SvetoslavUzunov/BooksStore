namespace BooksStore.Controllers;

using BooksStore.Data;
using BooksStore.Models;
using BooksStore.Models.Book;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

public class HomeController : Controller
{
    private readonly BooksStoreDbContext data;

    public HomeController(BooksStoreDbContext data)
        => this.data = data;

    public IActionResult Index()
    {
        //var books = data.Books
        //    .Select(b => new AllBooksViewModel
        //    {
        //        Id = b.Id,
        //        Title = b.Title,
        //        Description = b.Description,
        //        ImageUrl = b.ImageUrl,
        //        YearPublished = b.YearPublished,
        //        Price = b.Price,
        //        AuthorId = b.AuthorId,
        //        GenreId = b.GenreId
        //    })
        //    .ToList();

        //return View(books);
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}