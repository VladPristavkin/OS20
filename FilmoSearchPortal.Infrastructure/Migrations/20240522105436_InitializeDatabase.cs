using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FilmoSearchPortal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitializeDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b12b4391-ea81-41cb-9d0f-2b89c738b649");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e266bd1b-7f9a-4d13-8117-2d3a8e51ccfe");

            migrationBuilder.InsertData(
                table: "Actor",
                columns: new[] { "ActorId", "Biography", "Name" },
                values: new object[,]
                {
                    { 1, "Biography of Robert Downey Jr.", "Robert Downey Jr." },
                    { 2, "Biography of Scarlett Johansson", "Scarlett Johansson" },
                    { 3, "Biography of Chris Hemsworth", "Chris Hemsworth" }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3cb05fdf-ec3e-421e-af64-681c16996840", null, "Admin", "ADMIN" },
                    { "e9fcb8d1-1539-4203-9184-8c287ed0891a", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "Director",
                columns: new[] { "DirectorId", "Biography", "Name" },
                values: new object[,]
                {
                    { 1, "Biography of Steven Spielberg", "Steven Spielberg" },
                    { 2, "Biography of Christopher Nolan", "Christopher Nolan" }
                });

            migrationBuilder.InsertData(
                table: "Genre",
                columns: new[] { "GenreId", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Action movies", "Action" },
                    { 2, "Science Fiction movies", "Sci-Fi" }
                });

            migrationBuilder.InsertData(
                table: "Film",
                columns: new[] { "FilmId", "Description", "DirectorId", "Duration", "Rating", "ReleaseYear", "Title" },
                values: new object[,]
                {
                    { 1, "Description of Avengers: Endgame", 1, 181, 8.4f, 2019, "Avengers: Endgame" },
                    { 2, "Description of Inception", 2, 148, 8.8f, 2010, "Inception" }
                });

            migrationBuilder.InsertData(
                table: "ActorFilm",
                columns: new[] { "ActorId", "FilmId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 2 }
                });

            migrationBuilder.InsertData(
                table: "FilmGenre",
                columns: new[] { "FilmId", "GenreId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ActorFilm",
                keyColumns: new[] { "ActorId", "FilmId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "ActorFilm",
                keyColumns: new[] { "ActorId", "FilmId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "ActorFilm",
                keyColumns: new[] { "ActorId", "FilmId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3cb05fdf-ec3e-421e-af64-681c16996840");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e9fcb8d1-1539-4203-9184-8c287ed0891a");

            migrationBuilder.DeleteData(
                table: "FilmGenre",
                keyColumns: new[] { "FilmId", "GenreId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "FilmGenre",
                keyColumns: new[] { "FilmId", "GenreId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "FilmGenre",
                keyColumns: new[] { "FilmId", "GenreId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "Actor",
                keyColumn: "ActorId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Actor",
                keyColumn: "ActorId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Actor",
                keyColumn: "ActorId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Film",
                keyColumn: "FilmId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Film",
                keyColumn: "FilmId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "GenreId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "GenreId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Director",
                keyColumn: "DirectorId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Director",
                keyColumn: "DirectorId",
                keyValue: 2);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b12b4391-ea81-41cb-9d0f-2b89c738b649", null, "User", "USER" },
                    { "e266bd1b-7f9a-4d13-8117-2d3a8e51ccfe", null, "Admin", "ADMIN" }
                });
        }
    }
}
