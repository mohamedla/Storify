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
    public class MaterialGroupConfiguration : IEntityTypeConfiguration<MaterialGroup>
    {
        public void Configure(EntityTypeBuilder<MaterialGroup> builder)
        {
            builder.HasData(
                new MaterialGroup
                {
                    Id = new Guid("496edaa7-2a6e-481d-b160-c4a2e7333b87"),
                    Code = "Antibiotic",
                    GlobalName = "Antibiotics",
                    LocalName = "مضادات حيوية",
                    MTypeId = new Guid("496edaa7-b9eb-481d-b160-c4a2e7333b87")
                },
                new MaterialGroup
                {
                    Id = new Guid("c12bb473-7ef5-4d1b-aa11-ed60a12038b7"),
                    Code = "Painkiller",
                    GlobalName = "Painkillers",
                    LocalName = "مسكنات",
                    MTypeId = new Guid("496edaa7-b9eb-481d-b160-c4a2e7333b87")
                },
                new MaterialGroup
                {
                    Id = new Guid("3374144b-9876-45ba-aa75-d9a836a7a441"),
                    Code = "SparePart",
                    GlobalName = "Spare Parts",
                    LocalName = "قطع غيار",
                    MTypeId = new Guid("3374144b-ffdd-45ba-aa75-d9a836a7a441")
                },
                new MaterialGroup
                {
                    Id = new Guid("3374144b-ffdd-45ba-98ef-d9a836a7a441"),
                    Code = "Raw",
                    GlobalName = "Raw Materials",
                    LocalName = "مواد خام",
                    MTypeId = new Guid("3374144b-ffdd-45ba-aa75-d9a836a7a441")
                }
            );
        }
    }
}
