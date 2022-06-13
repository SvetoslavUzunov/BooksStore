using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BooksStore.Data.Migrations
{
    public partial class AddColumnBookIdToTableFavourites : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "Favourites",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Favourites");
        }
    }
}
