using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StrategicviewBack.Migrations
{
    public partial class InitialLocalTemplateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "administration");

            migrationBuilder.EnsureSchema(
                name: "security");

            migrationBuilder.CreateTable(
                name: "Companies",
                schema: "administration",
                columns: table => new
                {
                    company_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    industry = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    company_name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    country = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    state = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    city = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    postal_code = table.Column<int>(type: "int", nullable: true),
                    company_address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    company_phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    company_email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    manager_first_name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    manager_last_name = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    manager_phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manager_email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    contact_first_name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    contact_last_name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    contact_phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    contact_email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    contact_position = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    created_at = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.company_id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                schema: "security",
                columns: table => new
                {
                    permission_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    permission_name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    application_url = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    icon = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    is_parent = table.Column<bool>(type: "bit", nullable: true),
                    parent_permission_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.permission_id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "security",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    company_id = table.Column<int>(type: "int", nullable: true),
                    role_name = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.role_id);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                schema: "security",
                columns: table => new
                {
                    role_permission_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    permission_id = table.Column<int>(type: "int", nullable: true),
                    role_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => x.role_permission_id);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_permission_id",
                        column: x => x.permission_id,
                        principalSchema: "security",
                        principalTable: "Permissions",
                        principalColumn: "permission_id");
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_role_id",
                        column: x => x.role_id,
                        principalSchema: "security",
                        principalTable: "Roles",
                        principalColumn: "role_id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "security",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    role_id = table.Column<int>(type: "int", nullable: true),
                    project_id = table.Column<int>(type: "int", nullable: true),
                    first_name = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    last_name = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    identification_type = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    identification_number = table.Column<int>(type: "int", nullable: true),
                    username = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    password = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    email = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    phone = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    birth_date = table.Column<DateOnly>(type: "date", nullable: true),
                    company_joined_at = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.user_id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_role_id",
                        column: x => x.role_id,
                        principalSchema: "security",
                        principalTable: "Roles",
                        principalColumn: "role_id");
                });

            migrationBuilder.CreateTable(
                name: "CompanyUsers",
                schema: "security",
                columns: table => new
                {
                    company_user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    company_id = table.Column<int>(type: "int", nullable: true),
                    user_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyUsers", x => x.company_user_id);
                    table.ForeignKey(
                        name: "FK_CompanyUsers_Companies_company_id",
                        column: x => x.company_id,
                        principalSchema: "administration",
                        principalTable: "Companies",
                        principalColumn: "company_id");
                    table.ForeignKey(
                        name: "FK_CompanyUsers_Users_user_id",
                        column: x => x.user_id,
                        principalSchema: "security",
                        principalTable: "Users",
                        principalColumn: "user_id");
                });

            migrationBuilder.InsertData(
                schema: "administration",
                table: "Companies",
                columns: new[] { "company_id", "company_name", "industry", "country", "city", "company_email" },
                values: new object[] { 1, "Demo Company", "Software", "Local", "Local", "company@example.com" });

            migrationBuilder.InsertData(
                schema: "security",
                table: "Roles",
                columns: new[] { "role_id", "company_id", "role_name" },
                values: new object[] { 1, 1, "Admin" });

            migrationBuilder.InsertData(
                schema: "security",
                table: "Permissions",
                columns: new[] { "permission_id", "permission_name", "application_url", "icon", "is_parent", "parent_permission_id" },
                values: new object[,]
                {
                    { 1, "Administration", "/admin", "settings", true, null },
                    { 2, "Users", "/users", "users", false, 1 }
                });

            migrationBuilder.InsertData(
                schema: "security",
                table: "Users",
                columns: new[] { "user_id", "role_id", "first_name", "last_name", "username", "password", "email", "phone" },
                values: new object[] { 1, 1, "Admin", "Demo", "admin", "Admin123*", "admin@example.com", "0000000000" });

            migrationBuilder.InsertData(
                schema: "security",
                table: "CompanyUsers",
                columns: new[] { "company_user_id", "company_id", "user_id" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                schema: "security",
                table: "RolePermissions",
                columns: new[] { "role_permission_id", "permission_id", "role_id" },
                values: new object[] { 1, 2, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyUsers_company_id",
                schema: "security",
                table: "CompanyUsers",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyUsers_user_id",
                schema: "security",
                table: "CompanyUsers",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_permission_id",
                schema: "security",
                table: "RolePermissions",
                column: "permission_id");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_role_id",
                schema: "security",
                table: "RolePermissions",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_role_id",
                schema: "security",
                table: "Users",
                column: "role_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyUsers",
                schema: "security");

            migrationBuilder.DropTable(
                name: "RolePermissions",
                schema: "security");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "security");

            migrationBuilder.DropTable(
                name: "Companies",
                schema: "administration");

            migrationBuilder.DropTable(
                name: "Permissions",
                schema: "security");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "security");
        }
    }
}
