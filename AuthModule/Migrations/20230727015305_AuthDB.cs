using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthModule.Migrations
{
    /// <inheritdoc />
    public partial class AuthDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GeneralEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CompId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Auth_Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auth_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Auth_Companies_GeneralEntity_Id",
                        column: x => x.Id,
                        principalTable: "GeneralEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Auth_Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auth_Permissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Auth_Permissions_GeneralEntity_Id",
                        column: x => x.Id,
                        principalTable: "GeneralEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Auth_Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auth_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Auth_Roles_GeneralEntity_Id",
                        column: x => x.Id,
                        principalTable: "GeneralEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Auth_Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsEmailConfirmaed = table.Column<bool>(type: "bit", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassWord = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auth_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Auth_Users_GeneralEntity_Id",
                        column: x => x.Id,
                        principalTable: "GeneralEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Auth_UserPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    User_Id = table.Column<int>(type: "int", nullable: false),
                    Permission_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auth_UserPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Auth_UserPermissions_Auth_Permissions_Permission_Id",
                        column: x => x.Permission_Id,
                        principalTable: "Auth_Permissions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Auth_UserPermissions_Auth_Users_User_Id",
                        column: x => x.User_Id,
                        principalTable: "Auth_Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Auth_UserPermissions_GeneralEntity_Id",
                        column: x => x.Id,
                        principalTable: "GeneralEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Auth_UserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    User_Id = table.Column<int>(type: "int", nullable: false),
                    Role_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auth_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Auth_UserRoles_Auth_Roles_Role_Id",
                        column: x => x.Role_Id,
                        principalTable: "Auth_Roles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Auth_UserRoles_Auth_Users_User_Id",
                        column: x => x.User_Id,
                        principalTable: "Auth_Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Auth_UserRoles_GeneralEntity_Id",
                        column: x => x.Id,
                        principalTable: "GeneralEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Auth_UserTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    User_Id = table.Column<int>(type: "int", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auth_UserTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Auth_UserTokens_Auth_Users_User_Id",
                        column: x => x.User_Id,
                        principalTable: "Auth_Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Auth_UserTokens_GeneralEntity_Id",
                        column: x => x.Id,
                        principalTable: "GeneralEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Auth_UserPermissions_Permission_Id",
                table: "Auth_UserPermissions",
                column: "Permission_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Auth_UserPermissions_User_Id",
                table: "Auth_UserPermissions",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Auth_UserRoles_Role_Id",
                table: "Auth_UserRoles",
                column: "Role_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Auth_UserRoles_User_Id",
                table: "Auth_UserRoles",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Auth_UserTokens_User_Id",
                table: "Auth_UserTokens",
                column: "User_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Auth_Companies");

            migrationBuilder.DropTable(
                name: "Auth_UserPermissions");

            migrationBuilder.DropTable(
                name: "Auth_UserRoles");

            migrationBuilder.DropTable(
                name: "Auth_UserTokens");

            migrationBuilder.DropTable(
                name: "Auth_Permissions");

            migrationBuilder.DropTable(
                name: "Auth_Roles");

            migrationBuilder.DropTable(
                name: "Auth_Users");

            migrationBuilder.DropTable(
                name: "GeneralEntity");
        }
    }
}
