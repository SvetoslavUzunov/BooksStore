namespace BooksStore.Data;

using BooksStore.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class BooksStoreDbContext : IdentityDbContext
{
    public BooksStoreDbContext(DbContextOptions<BooksStoreDbContext> options)
        : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }

    public DbSet<Author> Authors { get; set; }

    public DbSet<Genre> Genres { get; set; }

    public DbSet<Favourite> Favourites { get; set; }
}