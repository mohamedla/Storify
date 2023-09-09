﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StorifyAPI.Context;

#nullable disable

namespace StorifyAPI.Migrations
{
    [DbContext(typeof(StorifyContext))]
    partial class StorifyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

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

                    b.Property<int>("matrialTypeID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.HasIndex("matrialTypeID");

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

                    b.Property<string>("LocalName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("NVARCHAR");

                    b.Property<int>("matrialGroupID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.HasIndex("matrialGroupID");

                    b.ToTable("MItems", (string)null);
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

            modelBuilder.Entity("StorifyAPI.Models.Matrial.MatrialGroup", b =>
                {
                    b.HasOne("StorifyAPI.Models.Matrial.MatrialType", "matrialType")
                        .WithMany("matrialGroups")
                        .HasForeignKey("matrialTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("matrialType");
                });

            modelBuilder.Entity("StorifyAPI.Models.Matrial.MatrialItem", b =>
                {
                    b.HasOne("StorifyAPI.Models.Matrial.MatrialGroup", "matrialGroup")
                        .WithMany("matrialItems")
                        .HasForeignKey("matrialGroupID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("matrialGroup");
                });

            modelBuilder.Entity("StorifyAPI.Models.Matrial.MatrialGroup", b =>
                {
                    b.Navigation("matrialItems");
                });

            modelBuilder.Entity("StorifyAPI.Models.Matrial.MatrialType", b =>
                {
                    b.Navigation("matrialGroups");
                });
#pragma warning restore 612, 618
        }
    }
}
