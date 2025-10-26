using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookContext.DL.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Books_CreatorId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_BookAuthors_BookId",
                table: "BookAuthors");

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthors_BookId_AuthorId",
                table: "BookAuthors",
                columns: new[] { "BookId", "AuthorId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BookAuthors_BookId_AuthorId",
                table: "BookAuthors");

            migrationBuilder.CreateIndex(
                name: "IX_Books_CreatorId",
                table: "Books",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthors_BookId",
                table: "BookAuthors",
                column: "BookId");
        }
    }
}
