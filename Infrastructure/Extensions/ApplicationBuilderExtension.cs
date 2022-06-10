namespace BooksStore.Infrastructure.Extensions;

using BooksStore.Data.Models;
using BooksStore.Data;
using Microsoft.EntityFrameworkCore;

public static class ApplicationBuilderExtension
{
    public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
    {
        using var scopedServices = app.ApplicationServices.CreateScope();
        var data = scopedServices.ServiceProvider.GetService<BooksStoreDbContext>();

        data.Database.Migrate();
        SeedGenres(data);
        SeedAuthors(data);

        return app;
    }

    private static void SeedGenres(BooksStoreDbContext data)
    {
        if (data.Genres.Any())
        {
            return;
        }

        data.Genres.AddRange(new[]
        {
            new Genre { Name = "Biography" },
            new Genre { Name = "Drama" },
            new Genre { Name = "Fantasy" },
            new Genre { Name = "Fiction" },
            new Genre { Name = "History" },
            new Genre { Name = "Romance" },
            new Genre { Name = "Teen" },
            new Genre { Name = "Thriller" },
            new Genre { Name = "Mystery" },
            new Genre { Name = "Self-Help" },
            new Genre { Name = "Poetry" },
            new Genre { Name = "Novel" },
            new Genre { Name = "Young adult" }
        });

        data.SaveChanges();
    }

    private static void SeedAuthors(BooksStoreDbContext data)
    {
        if (data.Authors.Any())
        {
            return;
        }

        data.Authors.AddRange(new[]
        {
            new Author{ Name = "Stephen King" },
            new Author{ Name = "J. K. Rowling" },
            new Author{ Name = "J. R. R. Tolken" },
            new Author{ Name = "Leigh Bardugo" },
            new Author{ Name = "Walter Isaacson" },
            new Author{ Name = "Robert Kiyosaki" },
            new Author{ Name = "Charles Dickens" },
            new Author{ Name = "Antoine de Saint-Exupéry" },
            new Author{ Name = "Agatha Christie" },
            new Author{ Name = "Dan Brown" },
            new Author{ Name = "John Green" },
            new Author{ Name = "Anne Frank" },
        });

        data.SaveChanges();
    }
}
