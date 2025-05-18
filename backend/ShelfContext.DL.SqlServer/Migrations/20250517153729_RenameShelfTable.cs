using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShelfContext.DL.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class RenameShelfTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Shelf",
                table: "Shelf");

            migrationBuilder.RenameTable(
                name: "Shelf",
                newName: "Shelves");

            migrationBuilder.RenameIndex(
                name: "IX_Shelf_UserId",
                table: "Shelves",
                newName: "IX_Shelves_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shelves",
                table: "Shelves",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Shelves",
                table: "Shelves");

            migrationBuilder.RenameTable(
                name: "Shelves",
                newName: "Shelf");

            migrationBuilder.RenameIndex(
                name: "IX_Shelves_UserId",
                table: "Shelf",
                newName: "IX_Shelf_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shelf",
                table: "Shelf",
                column: "Id");
        }
    }
}
