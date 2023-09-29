using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StorifyAPI.Migrations.Repository
{
    /// <inheritdoc />
    public partial class MaterialType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MaterialType",
                columns: table => new
                {
                    MTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "NVARCHAR(20)", maxLength: 20, nullable: false),
                    GlobalName = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    LocalName = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialType", x => x.MTypeID);
                });

            migrationBuilder.InsertData(
                table: "MaterialType",
                columns: new[] { "MTypeID", "Code", "GlobalName", "LocalName" },
                values: new object[,]
                {
                    { new Guid("3374144b-ffdd-45ba-aa75-d9a836a7a441"), "Consumptions", "Consumptions", "مستهلكات" },
                    { new Guid("496edaa7-b9eb-481d-b160-c4a2e7333b87"), "Drug", "Drugs", "أدوية" },
                    { new Guid("c12bb473-661f-4d1b-aa11-ed60a12038b7"), "Assets", "Fixed Assets", "أصول ثايتة" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaterialType_Code",
                table: "MaterialType",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaterialType");
        }
    }
}
