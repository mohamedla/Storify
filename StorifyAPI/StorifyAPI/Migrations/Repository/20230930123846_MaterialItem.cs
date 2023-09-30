using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StorifyAPI.Migrations.Repository
{
    /// <inheritdoc />
    public partial class MaterialItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MaterialItem",
                columns: table => new
                {
                    MItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "NVARCHAR(20)", maxLength: 20, nullable: false),
                    GlobalName = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    LocalName = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialItem", x => x.MItemId);
                    table.ForeignKey(
                        name: "FK_MaterialItem_MaterialGroup_MGroupId",
                        column: x => x.MGroupId,
                        principalTable: "MaterialGroup",
                        principalColumn: "MGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "MaterialItem",
                columns: new[] { "MItemId", "Code", "GlobalName", "LocalName", "MGroupId" },
                values: new object[,]
                {
                    { new Guid("3374144b-9876-78ef-aa75-d9a836a7a441"), "SparePart", "Spare Part", "قطعة غيار", new Guid("3374144b-9876-45ba-aa75-d9a836a7a441") },
                    { new Guid("3374144b-ad35-45ba-98ef-d9a836a7a441"), "Raw", "Raw Material", "مادة خام", new Guid("3374144b-ffdd-45ba-98ef-d9a836a7a441") },
                    { new Guid("496edaa7-2a6e-481d-abcd-c4a2e7333b87"), "Antibiotic", "Antibiotic", "مضاد حيوي", new Guid("496edaa7-2a6e-481d-b160-c4a2e7333b87") },
                    { new Guid("c12bb473-7ef5-4d1b-2245-ed60a12038b7"), "Painkiller", "Painkiller", "مسكن", new Guid("c12bb473-7ef5-4d1b-aa11-ed60a12038b7") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaterialItem_Code",
                table: "MaterialItem",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaterialItem_MGroupId",
                table: "MaterialItem",
                column: "MGroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaterialItem");
        }
    }
}
