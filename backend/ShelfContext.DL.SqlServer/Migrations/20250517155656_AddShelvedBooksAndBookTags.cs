using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShelfContext.DL.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class AddShelvedBooksAndBookTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShelvedBooks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShelfId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateShelved = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShelvedBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShelvedBooks_Shelves_ShelfId",
                        column: x => x.ShelfId,
                        principalTable: "Shelves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookTags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShelvedBookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookTags_ShelvedBooks_ShelvedBookId",
                        column: x => x.ShelvedBookId,
                        principalTable: "ShelvedBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookTags_ShelvedBookId",
                table: "BookTags",
                column: "ShelvedBookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookTags_TagId",
                table: "BookTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_ShelvedBooks_BookId",
                table: "ShelvedBooks",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_ShelvedBooks_ShelfId",
                table: "ShelvedBooks",
                column: "ShelfId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookTags");

            migrationBuilder.DropTable(
                name: "ShelvedBooks");
        }
    }
}
