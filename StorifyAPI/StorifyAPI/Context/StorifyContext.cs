using StorifyAPI.Models;
using Microsoft.EntityFrameworkCore;
using StorifyAPI.Models.Matrial;

namespace StorifyAPI.Context
{
    public class StorifyContext : DbContext
    {
        public StorifyContext(DbContextOptions<StorifyContext> options) : base(options) { }

        public virtual DbSet<MatrialType> matrialTypes { get; set; }
        public virtual DbSet<MatrialGroup> matrialGroups { get; set; }
        public virtual DbSet<MatrialItem> matrialItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MatrialType>()
                .ToTable("MTypes")
                .HasIndex( type => type.Code)
                .IsUnique();
            modelBuilder.Entity<MatrialGroup>()
                .ToTable("MGroups")
                .HasIndex(group => group.Code)
                .IsUnique();
            modelBuilder.Entity<MatrialItem>()
                .ToTable("MItems")
                .HasIndex(item => item.Code)
                .IsUnique();
        }
    }
}
