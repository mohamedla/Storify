using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Configuration
{
    public class StoreConfiguration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.HasData(
                
                new Store
                {
                    Id = new Guid("80abbca8-664d-4b20-b5de-024705497d4a"),
                    Code = "MainStore",
                    LocalName = "مخزن العناصر الرئيسى",
                    GlobalName = "Main Matrial Store"
                }

                );
        }
    }
}
