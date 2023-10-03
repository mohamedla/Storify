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
    public class MaterialItemUnitConfiguration : IEntityTypeConfiguration<MaterialItemUnit>
    {
        decimal unitPrice = 12.5M;
        public void Configure(EntityTypeBuilder<MaterialItemUnit> builder)
        {
            builder.HasData(
                new MaterialItemUnit
                {
                    Id = new Guid("496edaa7-98ce-e45d-90ae-c4a2e7333b87"),
                    ItemId = new Guid("496edaa7-2a6e-481d-abcd-c4a2e7333b87"),
                    UnitId = new Guid("c12bb473-adfe-4d1b-2245-ed60a12038b7"),
                    UnitPrice = unitPrice,
                    AveragePrice = 5,
                    LastPrice = 6,
                    CFactor = 7,
                    IsMain = true
                },
                new MaterialItemUnit
                {
                    Id = new Guid("496edaa7-98ce-e45d-a34d-c4a2e7333b87"),
                    ItemId = new Guid("496edaa7-2a6e-481d-abcd-c4a2e7333b87"),
                    UnitId = new Guid("496edaa7-76ea-481d-abcd-c4a2e7333b87"),
                    UnitPrice = unitPrice,
                    AveragePrice = 5,
                    LastPrice = 6,
                    CFactor = 7,
                    IsMain = false
                }
            );
        }
    }
}
