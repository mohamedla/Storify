using Entities.Models.Material;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Configuration.Material
{
    public class MaterialUnitConfiguration : IEntityTypeConfiguration<MaterialUnit>
    {
        public void Configure(EntityTypeBuilder<MaterialUnit> builder)
        {
            builder.HasData(
                new MaterialUnit
                {
                    Id = new Guid("496edaa7-76ea-481d-abcd-c4a2e7333b87"),
                    Code = "Box",
                    GlobalName = "Box ",
                    LocalName = "علبة"
                },
                new MaterialUnit
                {
                    Id = new Guid("c12bb473-adfe-4d1b-2245-ed60a12038b7"),
                    Code = "Bottle",
                    GlobalName = "Bottle",
                    LocalName = "زجاجة"
                },
                new MaterialUnit
                {
                    Id = new Guid("3374144b-1082-78ef-aa75-d9a836a7a441"),
                    Code = "Each",
                    GlobalName = "Each",
                    LocalName = "وحدة"
                },
                new MaterialUnit
                {
                    Id = new Guid("3374144b-ad35-45ba-afed-d9a836a7a441"),
                    Code = "Carton",
                    GlobalName = "Carton",
                    LocalName = "كارتونة"
                }
            );
        }
    }
}
