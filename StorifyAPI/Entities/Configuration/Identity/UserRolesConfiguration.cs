using Entities.Models.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Entities.Configuration.Identity
{
    public class UserRolesConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    UserId = new Guid("496edaa7-eadf-481d-89ea-9172e7333b87").ToString(),
                    RoleId = new Guid("496edaa7-eadf-481d-b160-c4a2e7888787").ToString()
                },
                new IdentityUserRole<string>
                {
                    UserId = new Guid("496edaa7-eadf-481d-89ea-9172e7333b87").ToString(),
                    RoleId = new Guid("496edaa7-1098-481d-b160-c4e489333b87").ToString()
                }
            );
        }
    }
}
