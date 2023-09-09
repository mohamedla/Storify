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
            modelBuilder.Entity<MatrialType>()
                .HasMany(type => type.matrialGroups)
                .WithOne(group => group.matrialType);

            modelBuilder.Entity<MatrialGroup>()
                .ToTable("MGroups")
                .HasIndex(group => group.Code)
                .IsUnique();
            modelBuilder.Entity<MatrialGroup>()
                .HasMany(group => group.matrialItems)
                .WithOne(item => item.matrialGroup);
            modelBuilder.Entity<MatrialGroup>()
                .HasOne(group => group.matrialType)
                .WithMany(type => type.matrialGroups)
                .HasForeignKey(group => group.TypeID);

            modelBuilder.Entity<MatrialItem>()
                .ToTable("MItems")
                .HasIndex(item => item.Code)
                .IsUnique();
            modelBuilder.Entity<MatrialItem>()
                .HasOne(item => item.matrialGroup)
                .WithMany(group => group.matrialItems)
                .HasForeignKey(item => item.GroupID);
        }
    }
}
