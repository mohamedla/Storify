using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StorifyAPI.Migrations.Identity
{
    /// <inheritdoc />
    public partial class AddAllRolesToAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "INSERT INTO [security].UserRoles (UserId, RoleId) select 'f5234a92-9055-4a03-a054-f5da6ec24523' ,Id from [security].[Roles]"
                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "Delete From [security].UserRoles where UserId = 'f5234a92-9055-4a03-a054-f5da6ec24523'"
                );
        }
    }
}
