using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class imdbID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserMovie_Movies_WatchedMoviesimdbId",
                table: "ApplicationUserMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_movieReviews_Movies_imdbID",
                table: "movieReviews");

            migrationBuilder.RenameColumn(
                name: "imdbID",
                table: "movieReviews",
                newName: "imdbID");

            migrationBuilder.RenameIndex(
                name: "IX_movieReviews_imdbID",
                table: "movieReviews",
                newName: "IX_movieReviews_imdbId");

            migrationBuilder.RenameColumn(
                name: "WatchedMoviesimdbId",
                table: "ApplicationUserMovie",
                newName: "WatchedMoviesimdbID");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUserMovie_WatchedMoviesimdbId",
                table: "ApplicationUserMovie",
                newName: "IX_ApplicationUserMovie_WatchedMoviesimdbID");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserMovie_Movies_WatchedMoviesimdbID",
                table: "ApplicationUserMovie",
                column: "WatchedMoviesimdbID",
                principalTable: "Movies",
                principalColumn: "imdbID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_movieReviews_Movies_imdbId",
                table: "movieReviews",
                column: "imdbID",
                principalTable: "Movies",
                principalColumn: "imdbID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserMovie_Movies_WatchedMoviesimdbID",
                table: "ApplicationUserMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_movieReviews_Movies_imdbId",
                table: "movieReviews");

            migrationBuilder.RenameColumn(
                name: "imdbID",
                table: "movieReviews",
                newName: "imdbID");

            migrationBuilder.RenameIndex(
                name: "IX_movieReviews_imdbId",
                table: "movieReviews",
                newName: "IX_movieReviews_imdbID");

            migrationBuilder.RenameColumn(
                name: "WatchedMoviesimdbID",
                table: "ApplicationUserMovie",
                newName: "WatchedMoviesimdbId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUserMovie_WatchedMoviesimdbID",
                table: "ApplicationUserMovie",
                newName: "IX_ApplicationUserMovie_WatchedMoviesimdbId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserMovie_Movies_WatchedMoviesimdbId",
                table: "ApplicationUserMovie",
                column: "WatchedMoviesimdbId",
                principalTable: "Movies",
                principalColumn: "imdbID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_movieReviews_Movies_imdbID",
                table: "movieReviews",
                column: "imdbID",
                principalTable: "Movies",
                principalColumn: "imdbID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
