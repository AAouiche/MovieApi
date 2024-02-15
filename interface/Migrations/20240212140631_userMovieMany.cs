using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class userMovieMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_AspNetUsers_ApplicationUserId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_ApplicationUserId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Movies");

            migrationBuilder.CreateTable(
                name: "ApplicationUserMovie",
                columns: table => new
                {
                    UsersId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WatchedMoviesimdbId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserMovie", x => new { x.UsersId, x.WatchedMoviesimdbId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserMovie_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserMovie_Movies_WatchedMoviesimdbId",
                        column: x => x.WatchedMoviesimdbId,
                        principalTable: "Movies",
                        principalColumn: "imdbID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserMovie_WatchedMoviesimdbId",
                table: "ApplicationUserMovie",
                column: "WatchedMoviesimdbId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserMovie");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Movies",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_ApplicationUserId",
                table: "Movies",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_AspNetUsers_ApplicationUserId",
                table: "Movies",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
