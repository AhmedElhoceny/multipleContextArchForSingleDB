using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthModule.Migrations
{
    /// <inheritdoc />
    public partial class AddIsSuperAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "Auth_Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSuperAdmin",
                table: "Auth_Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "Auth_Users");

            migrationBuilder.DropColumn(
                name: "IsSuperAdmin",
                table: "Auth_Users");
        }
    }
}
