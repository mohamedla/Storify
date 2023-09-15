using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StorifyAPI.Migrations.Identity
{
    /// <inheritdoc />
    public partial class ChangeColumnNameToIdentityUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FristName",
                schema: "security",
                table: "Users",
                newName: "FirstName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FirstName",
                schema: "security",
                table: "Users",
                newName: "FristName");
        }
    }
}
