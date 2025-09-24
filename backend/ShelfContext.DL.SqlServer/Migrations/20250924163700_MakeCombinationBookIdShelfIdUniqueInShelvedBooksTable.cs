using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShelfContext.DL.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class MakeCombinationBookIdShelfIdUniqueInShelvedBooksTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ShelvedBooks_UserId",
                table: "ShelvedBooks");

            migrationBuilder.CreateIndex(
                name: "IX_ShelvedBooks_UserId_BookId",
                table: "ShelvedBooks",
                columns: new[] { "UserId", "BookId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ShelvedBooks_UserId_BookId",
                table: "ShelvedBooks");

            migrationBuilder.CreateIndex(
                name: "IX_ShelvedBooks_UserId",
                table: "ShelvedBooks",
                column: "UserId");
        }
    }
}
