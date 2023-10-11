using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StorifyAPI.Migrations.Repository
{
    /// <inheritdoc />
    public partial class UserAndRoleMetaData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "secure",
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "496edaa7-1098-481d-b160-c4e489333b87", "496feac7-eadf-aefd-9786-c4a2e7333b87", "User", "USER" },
                    { "496edaa7-eadf-481d-b160-c4a2e7888787", "496edaa7-eadf-aefd-b160-c4a2e7557487", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                schema: "secure",
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IsActive", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "496edaa7-eadf-481d-89ea-9172e7333b87", 0, "496edaa7-eadf-8a02-b160-c4a2e7333b87", "Admin@Storify.com", false, "Administrator", true, "UserRole", true, null, "ADMIN@STORIFY.COM", "ADMIN", "AQAAAAEAACcQAAAAEMLMO+FoeOqPzBHBaSoxn+4Abu7aXjrkbwc65ehRXxLEGgNhaOB6Ln17ud6ZUKvwPQ==", "0201122216440", false, "496edaa7-eadf-5ade-8195-c4a2e7333b87", false, "Admin" });

            migrationBuilder.InsertData(
                schema: "secure",
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "496edaa7-1098-481d-b160-c4e489333b87", "496edaa7-eadf-481d-89ea-9172e7333b87" },
                    { "496edaa7-eadf-481d-b160-c4a2e7888787", "496edaa7-eadf-481d-89ea-9172e7333b87" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "secure",
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "496edaa7-1098-481d-b160-c4e489333b87", "496edaa7-eadf-481d-89ea-9172e7333b87" });

            migrationBuilder.DeleteData(
                schema: "secure",
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "496edaa7-eadf-481d-b160-c4a2e7888787", "496edaa7-eadf-481d-89ea-9172e7333b87" });

            migrationBuilder.DeleteData(
                schema: "secure",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "496edaa7-1098-481d-b160-c4e489333b87");

            migrationBuilder.DeleteData(
                schema: "secure",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "496edaa7-eadf-481d-b160-c4a2e7888787");

            migrationBuilder.DeleteData(
                schema: "secure",
                table: "Users",
                keyColumn: "Id",
                keyValue: "496edaa7-eadf-481d-89ea-9172e7333b87");
        }
    }
}
