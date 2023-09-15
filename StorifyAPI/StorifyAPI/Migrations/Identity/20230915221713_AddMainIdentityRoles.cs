using Microsoft.EntityFrameworkCore.Migrations;
using StorifyAPI.Models.Employee;
using System.Data;

#nullable disable

namespace StorifyAPI.Migrations.Identity
{
    /// <inheritdoc />
    public partial class AddMainIdentityRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
            values: new object[] { Guid.NewGuid().ToString(), RoleNames.user, RoleNames.user.ToUpper(), Guid.NewGuid().ToString() },
            schema: "security"
            );
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
            values: new object[] { Guid.NewGuid().ToString(), RoleNames.admin, RoleNames.admin.ToUpper(), Guid.NewGuid().ToString() },
            schema: "security"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "DELETE FROM [security].[Roles]"
                );
        }
    }
}
