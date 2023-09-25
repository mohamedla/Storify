using Entities.Configuration;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext( DbContextOptions<RepositoryContext> options) : base(options)
        { }

        public DbSet<Store> Stores { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StoreConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());

            modelBuilder.Entity<Store>()
                .HasIndex(s => s.Code)
                .IsUnique();

            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.Code)
                .IsUnique();
        }
    }
}
