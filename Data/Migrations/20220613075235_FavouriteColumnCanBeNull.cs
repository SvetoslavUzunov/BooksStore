using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BooksStore.Data.Migrations
{
    public partial class FavouriteColumnCanBeNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Favourites_FavouriteId",
                table: "Books");

            migrationBuilder.AlterColumn<int>(
                name: "FavouriteId",
                table: "Books",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Favourites_FavouriteId",
                table: "Books",
                column: "FavouriteId",
                principalTable: "Favourites",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Favourites_FavouriteId",
                table: "Books");

            migrationBuilder.AlterColumn<int>(
                name: "FavouriteId",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Favourites_FavouriteId",
                table: "Books",
                column: "FavouriteId",
                principalTable: "Favourites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
