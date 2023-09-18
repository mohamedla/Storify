﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StorifyAPI.Context;

#nullable disable

namespace StorifyAPI.Migrations
{
    [DbContext(typeof(StorifyContext))]
    [Migration("20230912031650_AddMIUIDColumnAutoIncre")]
    partial class AddMIUIDColumnAutoIncre
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MatrialItemMatrialUnit", b =>
                {
                    b.Property<int>("matrialItemsID")
                        .HasColumnType("int");

                    b.Property<int>("matrialUnitsID")
                        .HasColumnType("int");

                    b.HasKey("matrialItemsID", "matrialUnitsID");

                    b.HasIndex("matrialUnitsID");

                    b.ToTable("MatrialItemMatrialUnit");
                });

            modelBuilder.Entity("StorifyAPI.Models.Matrial.MatrialGroup", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("MGroupID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR");

                    b.Property<string>("GlobalName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("NVARCHAR");

                    b.Property<string>("LocalName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("NVARCHAR");

                    b.Property<int>("TypeID")
                        .HasColumnType("int")
                        .HasColumnName("MTypeID");

                    b.HasKey("ID");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.HasIndex("TypeID");

                    b.ToTable("MGroups", (string)null);
                });

            modelBuilder.Entity("StorifyAPI.Models.Matrial.MatrialItem", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("MItemID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR");

                    b.Property<string>("GlobalName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("NVARCHAR");

                    b.Property<int>("GroupID")
                        .HasColumnType("int")
                        .HasColumnName("MGroupID");

                    b.Property<string>("LocalName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("NVARCHAR");

                    b.HasKey("ID");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.HasIndex("GroupID");

                    b.ToTable("MItems", (string)null);
                });

            modelBuilder.Entity("StorifyAPI.Models.Matrial.MatrialItemUnit", b =>
                {
                    b.Property<int>("UnitID")
                        .HasColumnType("int")
                        .HasColumnName("MUnitID");

                    b.Property<int>("ItemID")
                        .HasColumnType("int")
                        .HasColumnName("MItemID");

                    b.Property<int>("CFactor")
                        .HasColumnType("int");

                    b.Property<int>("CUnitID")
                        .HasColumnType("int")
                        .HasColumnName("MCUnitID");

                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("MItemUnitID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("Money");

                    b.Property<bool>("isMain")
                        .HasColumnType("bit");

                    b.HasKey("UnitID", "ItemID");

                    b.HasIndex("CUnitID");

                    b.HasIndex("ItemID");

                    b.ToTable("MItemUnit", (string)null);
                });

            modelBuilder.Entity("StorifyAPI.Models.Matrial.MatrialType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("MTypeID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR");

                    b.Property<string>("GlobalName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("NVARCHAR");

                    b.Property<string>("LocalName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("NVARCHAR");

                    b.HasKey("ID");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("MTypes", (string)null);
                });

            modelBuilder.Entity("StorifyAPI.Models.Matrial.MatrialUnit", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("MUnitID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR");

                    b.Property<string>("GlobalName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("NVARCHAR");

                    b.Property<string>("LocalName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("NVARCHAR");

                    b.HasKey("ID");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("MUnits", (string)null);
                });

            modelBuilder.Entity("MatrialItemMatrialUnit", b =>
                {
                    b.HasOne("StorifyAPI.Models.Matrial.MatrialItem", null)
                        .WithMany()
                        .HasForeignKey("matrialItemsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StorifyAPI.Models.Matrial.MatrialUnit", null)
                        .WithMany()
                        .HasForeignKey("matrialUnitsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StorifyAPI.Models.Matrial.MatrialGroup", b =>
                {
                    b.HasOne("StorifyAPI.Models.Matrial.MatrialType", "matrialType")
                        .WithMany("matrialGroups")
                        .HasForeignKey("TypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("matrialType");
                });

            modelBuilder.Entity("StorifyAPI.Models.Matrial.MatrialItem", b =>
                {
                    b.HasOne("StorifyAPI.Models.Matrial.MatrialGroup", "matrialGroup")
                        .WithMany("matrialItems")
                        .HasForeignKey("GroupID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("matrialGroup");
                });

            modelBuilder.Entity("StorifyAPI.Models.Matrial.MatrialItemUnit", b =>
                {
                    b.HasOne("StorifyAPI.Models.Matrial.MatrialUnit", "CMatrialUnit")
                        .WithMany("CmatrialItemsUnit")
                        .HasForeignKey("CUnitID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("StorifyAPI.Models.Matrial.MatrialItem", "MatrialItem")
                        .WithMany("matrialItemUnits")
                        .HasForeignKey("ItemID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("StorifyAPI.Models.Matrial.MatrialUnit", "MatrialUnit")
                        .WithMany("matrialItemsUnit")
                        .HasForeignKey("UnitID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("CMatrialUnit");

                    b.Navigation("MatrialItem");

                    b.Navigation("MatrialUnit");
                });

            modelBuilder.Entity("StorifyAPI.Models.Matrial.MatrialGroup", b =>
                {
                    b.Navigation("matrialItems");
                });

            modelBuilder.Entity("StorifyAPI.Models.Matrial.MatrialItem", b =>
                {
                    b.Navigation("matrialItemUnits");
                });

            modelBuilder.Entity("StorifyAPI.Models.Matrial.MatrialType", b =>
                {
                    b.Navigation("matrialGroups");
                });

            modelBuilder.Entity("StorifyAPI.Models.Matrial.MatrialUnit", b =>
                {
                    b.Navigation("CmatrialItemsUnit");

                    b.Navigation("matrialItemsUnit");
                });
#pragma warning restore 612, 618
        }
    }
}
