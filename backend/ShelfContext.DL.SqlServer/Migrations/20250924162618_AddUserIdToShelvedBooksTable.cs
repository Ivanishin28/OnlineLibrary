using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShelfContext.DL.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToShelvedBooksTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "ShelvedBooks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ShelvedBooks_UserId",
                table: "ShelvedBooks",
                column: "UserId");

            migrationBuilder.Sql("""
                UPDATE sb
                SET sb.UserId = s.UserId
                FROM dbo.ShelvedBooks sb
                JOIN dbo.Shelves s
                    ON sb.ShelfId = s.Id;
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ShelvedBooks_UserId",
                table: "ShelvedBooks");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ShelvedBooks");
        }
    }
}
