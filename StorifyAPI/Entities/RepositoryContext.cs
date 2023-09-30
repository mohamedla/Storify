using Entities.Configuration;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models.Material;

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
            #region Config
            modelBuilder.ApplyConfiguration(new StoreConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new MaterialTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MaterialGroupConfiguration());
            modelBuilder.ApplyConfiguration(new MaterialItemConfiguration());
            #endregion

            #region Material
            modelBuilder.Entity<MaterialType>()
                    .HasIndex(t => t.Code)
                    .IsUnique();

            modelBuilder.Entity<MaterialGroup>()
                .HasIndex(g => g.Code)
                .IsUnique();

            modelBuilder.Entity<MaterialItem>()
                .HasIndex(i => i.Code)
                .IsUnique();
            #endregion

            modelBuilder.Entity<Store>()
                .HasIndex(s => s.Code)
                .IsUnique();

            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.Code)
                .IsUnique();
        }
    }
}
