using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookContext.DL.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class AddVisibilityToBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Visibility",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Visibility",
                table: "Books");
        }
    }
}
