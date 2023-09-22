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
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasData(
                
                new Employee
                {
                    Id = new Guid("f9e4c537-87d6-810f-9a73-2754e9111870"),
                    Code = "299159",
                    Name = "Mohamed Ashraf",
                    Age = 24,
                    StoreId = new Guid("80abbca8-664d-4b20-b5de-024705497d4a")
                }

                );
        }
    }
}
