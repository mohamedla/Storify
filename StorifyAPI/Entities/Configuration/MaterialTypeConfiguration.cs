using Entities.Models.Material;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Configuration
{
    public class MaterialTypeConfiguration : IEntityTypeConfiguration<MaterialType>
    {
        public void Configure(EntityTypeBuilder<MaterialType> builder)
        {
            builder.HasData(
                new MaterialType
                {
                    Id = new Guid("496edaa7-b9eb-481d-b160-c4a2e7333b87"),
                    Code = "Drug",
                    GlobalName = "Drugs",
                    LocalName = "أدوية"
                },
                new MaterialType
                {
                    Id = new Guid("c12bb473-661f-4d1b-aa11-ed60a12038b7"),
                    Code = "Assets",
                    GlobalName = "Fixed Assets",
                    LocalName = "أصول ثايتة"
                },
                new MaterialType
                {
                    Id = new Guid("3374144b-ffdd-45ba-aa75-d9a836a7a441"),
                    Code = "Consumptions",
                    GlobalName = "Consumptions",
                    LocalName = "مستهلكات"
                }
            );
        }
    }
}
