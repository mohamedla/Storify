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
                    Id = Guid.NewGuid(),
                    Code = "Drug",
                    GlobalName = "Drugs",
                    LocalName = "أدوية"
                },
                new MaterialType
                {
                    Id = Guid.NewGuid(),
                    Code = "Assets",
                    GlobalName = "Fixed Assets",
                    LocalName = "أصول ثايتة"
                },
                new MaterialType
                {
                    Id = Guid.NewGuid(),
                    Code = "Consumptions",
                    GlobalName = "Consumptions",
                    LocalName = "مستهلكات"
                }
            );
        }
    }
}
