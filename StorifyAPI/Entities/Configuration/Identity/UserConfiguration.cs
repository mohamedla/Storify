using Entities.Models.Identity;
using Entities.Models.Material;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Configuration.Identity
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User
                {
                    Id = new Guid("496edaa7-eadf-481d-89ea-9172e7333b87").ToString(),
                    UserName = "Admin",
                    NormalizedUserName = "Admin".ToUpper(),
                    Email = "Admin@Storify.com",
                    NormalizedEmail = "Admin@Storify.com".ToUpper(),
                    EmailConfirmed = false,
                    PasswordHash = "AQAAAAEAACcQAAAAEMLMO+FoeOqPzBHBaSoxn+4Abu7aXjrkbwc65ehRXxLEGgNhaOB6Ln17ud6ZUKvwPQ==",// Admin_123456
                    SecurityStamp = new Guid("496edaa7-eadf-5ade-8195-c4a2e7333b87").ToString(),
                    ConcurrencyStamp = new Guid("496edaa7-eadf-8a02-b160-c4a2e7333b87").ToString(),
                    PhoneNumber = "0201122216440",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    FirstName = "Administrator",
                    LastName = "UserRole",
                    IsActive = true,
                }
            );
        }
    }
}
