using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CineVerse.api.Migrations
{
    /// <inheritdoc />
    public partial class CreateGenres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 12, "Adventure" },
                    { 14, "Fantasy" },
                    { 16, "Animation" },
                    { 18, "Drama" },
                    { 27, "Horror" },
                    { 28, "Action" },
                    { 35, "Comedy" },
                    { 36, "History" },
                    { 37, "Western" },
                    { 53, "Thriller" },
                    { 80, "Crime" },
                    { 99, "Documentary" },
                    { 878, "Science Fiction" },
                    { 9648, "Mystery" },
                    { 10402, "Music" },
                    { 10749, "Romance" },
                    { 10751, "Family" },
                    { 10752, "War" },
                    { 10770, "TV Movie" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Genres");
        }
    }
}
