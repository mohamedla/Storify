using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StorifyAPI.Migrations.Repository
{
    /// <inheritdoc />
    public partial class MaterialUnit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MaterialUnit",
                columns: table => new
                {
                    MUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "NVARCHAR(20)", maxLength: 20, nullable: false),
                    GlobalName = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    LocalName = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialUnit", x => x.MUnitId);
                });

            migrationBuilder.InsertData(
                table: "MaterialUnit",
                columns: new[] { "MUnitId", "Code", "GlobalName", "LocalName" },
                values: new object[,]
                {
                    { new Guid("3374144b-1082-78ef-aa75-d9a836a7a441"), "Each", "Each", "وحدة" },
                    { new Guid("3374144b-ad35-45ba-afed-d9a836a7a441"), "Carton", "Carton", "كارتونة" },
                    { new Guid("496edaa7-76ea-481d-abcd-c4a2e7333b87"), "Box", "Box ", "علبة" },
                    { new Guid("c12bb473-adfe-4d1b-2245-ed60a12038b7"), "Bottle", "Bottle", "زجاجة" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaterialUnit_Code",
                table: "MaterialUnit",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaterialUnit");
        }
    }
}
