using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookContext.DL.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class AddBookMetadata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookMetadatas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PublishingDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CoverId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookMetadatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookMetadatas_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookMetadatas_BookId",
                table: "BookMetadatas",
                column: "BookId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookMetadatas");
        }
    }
}
