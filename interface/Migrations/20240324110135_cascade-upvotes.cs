using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class cascadeupvotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Upvotes_movieReviews_ReviewId",
                table: "Upvotes");

            migrationBuilder.AddForeignKey(
                name: "FK_Upvotes_movieReviews_ReviewId",
                table: "Upvotes",
                column: "ReviewId",
                principalTable: "movieReviews",
                principalColumn: "ReviewId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Upvotes_movieReviews_ReviewId",
                table: "Upvotes");

            migrationBuilder.AddForeignKey(
                name: "FK_Upvotes_movieReviews_ReviewId",
                table: "Upvotes",
                column: "ReviewId",
                principalTable: "movieReviews",
                principalColumn: "ReviewId");
        }
    }
}
