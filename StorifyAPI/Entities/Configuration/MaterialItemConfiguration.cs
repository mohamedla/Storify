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
    public class MaterialItemConfiguration : IEntityTypeConfiguration<MaterialItem>
    {
        public void Configure(EntityTypeBuilder<MaterialItem> builder)
        {
            builder.HasData(
                new MaterialItem
                {
                    Id = new Guid("496edaa7-2a6e-481d-abcd-c4a2e7333b87"),
                    Code = "Antibiotic",
                    GlobalName = "Antibiotic",
                    LocalName = "مضاد حيوي",
                    MGroupId = new Guid("496edaa7-2a6e-481d-b160-c4a2e7333b87")
                },
                new MaterialItem
                {
                    Id = new Guid("c12bb473-7ef5-4d1b-2245-ed60a12038b7"),
                    Code = "Painkiller",
                    GlobalName = "Painkiller",
                    LocalName = "مسكن",
                    MGroupId = new Guid("c12bb473-7ef5-4d1b-aa11-ed60a12038b7")
                },
                new MaterialItem
                {
                    Id = new Guid("3374144b-9876-78ef-aa75-d9a836a7a441"),
                    Code = "SparePart",
                    GlobalName = "Spare Part",
                    LocalName = "قطعة غيار",
                    MGroupId = new Guid("3374144b-9876-45ba-aa75-d9a836a7a441")
                },
                new MaterialItem
                {
                    Id = new Guid("3374144b-ad35-45ba-98ef-d9a836a7a441"),
                    Code = "Raw",
                    GlobalName = "Raw Material",
                    LocalName = "مادة خام",
                    MGroupId = new Guid("3374144b-ffdd-45ba-98ef-d9a836a7a441")
                }
            );
        }
    }
}
