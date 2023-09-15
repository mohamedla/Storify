using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StorifyAPI.Migrations.Identity
{
    /// <inheritdoc />
    public partial class AddToIdentityUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "INSERT INTO [security].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [PersonalImage]) VALUES (N'f5234a92-9055-4a03-a054-f5da6ec24523', N'Admin', N'ADMIN', N'Admin@Storify.com', N'ADMIN@STORIFY.COM', 0, N'AQAAAAEAACcQAAAAEMLMO+FoeOqPzBHBaSoxn+4Abu7aXjrkbwc65ehRXxLEGgNhaOB6Ln17ud6ZUKvwPQ==', N'3ZZB6ND7M5LVO2UCNV7YJXTLNTKNXRFV', N'eba73e1e-f392-4e0c-8ebe-452e1cece7f5', NULL, 0, 0, NULL, 1, 0, N'Administrator', N'Userole', NULL)"
                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "DELETE TABLE [security].[Users] WHERE [Id] = 'f5234a92-9055-4a03-a054-f5da6ec24523'"
                );
        }
    }
}
