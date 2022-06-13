using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BooksStore.Data.Migrations
{
    public partial class AddFavouriteTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FavouriteId",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Favourites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favourites", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_FavouriteId",
                table: "Books",
                column: "FavouriteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Favourites_FavouriteId",
                table: "Books",
                column: "FavouriteId",
                principalTable: "Favourites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Favourites_FavouriteId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "Favourites");

            migrationBuilder.DropIndex(
                name: "IX_Books_FavouriteId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "FavouriteId",
                table: "Books");
        }
    }
}
