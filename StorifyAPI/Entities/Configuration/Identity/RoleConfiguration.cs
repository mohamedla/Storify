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
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = new Guid("496edaa7-eadf-481d-b160-c4a2e7888787").ToString(),
                    Name = RoleNames.admin,
                    NormalizedName = RoleNames.admin.ToUpper(),
                    ConcurrencyStamp = new Guid("496edaa7-eadf-aefd-b160-c4a2e7557487").ToString(),
                },
                new IdentityRole
                {
                    Id = new Guid("496edaa7-1098-481d-b160-c4e489333b87").ToString(),
                    Name = RoleNames.user,
                    NormalizedName = RoleNames.user.ToUpper(),
                    ConcurrencyStamp = new Guid("496feac7-eadf-aefd-9786-c4a2e7333b87").ToString(),
                }
            );
        }
    }
}
