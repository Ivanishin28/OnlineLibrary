using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediaContext.DL.Migrations
{
    /// <inheritdoc />
    public partial class AddContentTypeToMediaFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cotnext",
                table: "MediaFiles",
                newName: "Content");

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "MediaFiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "MediaFiles");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "MediaFiles",
                newName: "Cotnext");
        }
    }
}
