using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityContext.DL.Migrations
{
    /// <inheritdoc />
    public partial class AddAvatarIdToIdentityUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AvatarId",
                table: "ApplicationUsers",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarId",
                table: "ApplicationUsers");
        }
    }
}
