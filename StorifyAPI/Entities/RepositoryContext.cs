using Entities.Configuration;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models.Material;
using Entities.Configuration.Material;

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
            modelBuilder.ApplyConfiguration(new MaterialUnitConfiguration());
            modelBuilder.ApplyConfiguration(new MaterialItemUnitConfiguration());
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

            modelBuilder.Entity<MaterialUnit>()
                .HasIndex(u => u.Code)
                .IsUnique();

            modelBuilder.Entity<MaterialItemUnit>()
                .HasIndex(iu => new { iu.ItemId, iu.UnitId})
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
