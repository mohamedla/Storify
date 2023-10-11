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
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Entities.Models.Identity;
using Entities.Configuration.Identity;

namespace Entities
{
    public class RepositoryContext : IdentityDbContext<User>
    {
        public RepositoryContext( DbContextOptions<RepositoryContext> options) : base(options)
        { }

        public DbSet<Store> Stores { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Identity

            modelBuilder.Entity<User>().ToTable("Users", "secure");

            modelBuilder.Entity<IdentityRole>() .ToTable("Roles", "secure");

            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "secure");

            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "secure");

            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "secure");

            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim", "secure");

            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "secure");
            #endregion

            #region Config
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserRolesConfiguration());

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
