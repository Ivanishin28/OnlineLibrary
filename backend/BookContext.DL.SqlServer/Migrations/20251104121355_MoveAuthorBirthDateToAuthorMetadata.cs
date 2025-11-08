using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookContext.DL.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class MoveAuthorBirthDateToAuthorMetadata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookAuthor_Authors_AuthorId",
                table: "BookAuthor");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Authors");

            migrationBuilder.AddColumn<DateOnly>(
                name: "BirthDate",
                table: "AuthorMetadatas",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddForeignKey(
                name: "FK_BookAuthor_Authors_AuthorId",
                table: "BookAuthor",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookAuthor_Authors_AuthorId",
                table: "BookAuthor");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "AuthorMetadatas");

            migrationBuilder.AddColumn<DateOnly>(
                name: "BirthDate",
                table: "Authors",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddForeignKey(
                name: "FK_BookAuthor_Authors_AuthorId",
                table: "BookAuthor",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
