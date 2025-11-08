using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookContext.DL.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class AddFileIdToBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FileId",
                table: "BookMetadatas",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileId",
                table: "BookMetadatas");
        }
    }
}
