using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShelfContext.DL.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class RemoveShelvedBookIdFromReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_ShelvedBooks_ShelvedBookId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ShelvedBookId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ShelvedBookId",
                table: "Reviews");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ShelvedBookId",
                table: "Reviews",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ShelvedBookId",
                table: "Reviews",
                column: "ShelvedBookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_ShelvedBooks_ShelvedBookId",
                table: "Reviews",
                column: "ShelvedBookId",
                principalTable: "ShelvedBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
