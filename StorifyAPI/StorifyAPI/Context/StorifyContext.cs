using StorifyAPI.Models;
using Microsoft.EntityFrameworkCore;
using StorifyAPI.Models.Matrial;
//using StorifyAPI.Models.Stores;
//using StorifyAPI.Models.Employee;

namespace StorifyAPI.Context
{
    public class StorifyContext : DbContext
    {
        public StorifyContext(DbContextOptions<StorifyContext> options) : base(options) { }

        public virtual DbSet<MatrialType> matrialTypes { get; set; }
        public virtual DbSet<MatrialGroup> matrialGroups { get; set; }
        public virtual DbSet<MatrialItem> matrialItems { get; set; }
        public virtual DbSet<MatrialUnit> matrialUnits { get; set; }
        public virtual DbSet<MatrialItemUnit> matrialItemUnits { get; set; }
        //public virtual DbSet<CostCenter> CostCenters { get; set; }
        //public virtual DbSet<Storage> Stores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Matrial Type
            modelBuilder.Entity<MatrialType>()
                   .ToTable("MTypes")
                   .HasIndex(type => type.Code)
                   .IsUnique();
            modelBuilder.Entity<MatrialType>()
                .HasMany(type => type.matrialGroups)
                .WithOne(group => group.matrialType);
            #endregion

            #region Matrial Group
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
            #endregion

            #region Matrial Item
            modelBuilder.Entity<MatrialItem>()
                   .ToTable("MItems")
                   .HasIndex(item => item.Code)
                   .IsUnique();
            modelBuilder.Entity<MatrialItem>()
                .HasOne(item => item.matrialGroup)
                .WithMany(group => group.matrialItems)
                .HasForeignKey(item => item.GroupID);
            modelBuilder.Entity<MatrialItem>()
                .HasMany(item => item.matrialUnits)
                .WithMany(unit => unit.matrialItems);
            #endregion

            #region Matrial Unit
            modelBuilder.Entity<MatrialUnit>()
                    .ToTable("MUnits")
                    .HasIndex(unit => unit.Code)
                    .IsUnique();
            #endregion

            #region Matrial Item Units
            modelBuilder.Entity<MatrialItemUnit>()
                   .ToTable("MItemUnit")
                   .HasKey("UnitID", "ItemID");

            modelBuilder.Entity<MatrialItemUnit>()
                .HasOne(iu => iu.MatrialUnit)
                .WithMany(u => u.matrialItemsUnit)
                .HasForeignKey(iu => iu.UnitID)
                .HasPrincipalKey(nameof(MatrialUnit.ID))
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MatrialItemUnit>()
                .HasOne(iu => iu.MatrialItem)
                .WithMany(i => i.matrialItemUnits)
                .HasForeignKey(iu => iu.ItemID)
                .HasPrincipalKey(nameof(MatrialItem.ID))
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MatrialItemUnit>()
                .HasOne(iu => iu.CMatrialUnit)
                .WithMany(u => u.CmatrialItemsUnit)
                .HasForeignKey(iu => iu.CUnitID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MatrialItemUnit>()
                .Property(ItemUnit => ItemUnit.ID)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<MatrialItemUnit>()
                .HasKey(iu => new { iu.UnitID, iu.ItemID });
            #endregion

            //#region CostCenter
            //modelBuilder.Entity<CostCenter>()
            //       .ToTable("SCostCenters")
            //       .HasIndex(CC => CC.Code)
            //       .IsUnique();
            //modelBuilder.Entity<CostCenter>()
            //    .HasMany(CC => CC.Stores)
            //    .WithOne(store => store.CostCenter);
            //#endregion

            //#region Store
            //modelBuilder.Entity<Storage>()
            //       .ToTable("Stores")
            //       .HasIndex(store => store.Code)
            //       .IsUnique();
            //modelBuilder.Entity<Storage>()
            //    .HasOne(store => store.CostCenter)
            //    .WithMany(CC => CC.Stores)
            //    .HasForeignKey(store => store.CostCenterID);
            //#endregion

            //#region Employee
            //modelBuilder.Entity<Employee>()
            //      .ToTable("Employees")
            //      .HasIndex(emp => emp.Code)
            //      .IsUnique();
            //modelBuilder.Entity<Employee>()
            //    .HasOne(emp => emp.Role)
            //    .WithMany(role => role.Employees)
            //    .HasForeignKey(emp => emp.RoleID);
            //#endregion

            //#region Roles
            //modelBuilder.Entity<EmployeeRole>()
            //      .ToTable("EmpRoles")
            //      .HasIndex(emp => emp.Code)
            //      .IsUnique();
            //modelBuilder.Entity<EmployeeRole>()
            //    .HasMany(role => role.Employees)
            //    .WithOne(emp => emp.Role);
            //#endregion
        }
    }
}
