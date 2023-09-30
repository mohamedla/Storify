using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StorifyAPI.Migrations.Repository
{
    /// <inheritdoc />
    public partial class MaterialGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MaterialGroup",
                columns: table => new
                {
                    MGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "NVARCHAR(20)", maxLength: 20, nullable: false),
                    GlobalName = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    LocalName = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialGroup", x => x.MGroupId);
                    table.ForeignKey(
                        name: "FK_MaterialGroup_MaterialType_MTypeId",
                        column: x => x.MTypeId,
                        principalTable: "MaterialType",
                        principalColumn: "MTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "MaterialGroup",
                columns: new[] { "MGroupId", "Code", "GlobalName", "LocalName", "MTypeId" },
                values: new object[,]
                {
                    { new Guid("3374144b-9876-45ba-aa75-d9a836a7a441"), "SparePart", "Spare Parts", "قطع غيار", new Guid("3374144b-ffdd-45ba-aa75-d9a836a7a441") },
                    { new Guid("3374144b-ffdd-45ba-98ef-d9a836a7a441"), "Raw", "Raw Materials", "مواد خام", new Guid("3374144b-ffdd-45ba-aa75-d9a836a7a441") },
                    { new Guid("496edaa7-2a6e-481d-b160-c4a2e7333b87"), "Antibiotic", "Antibiotics", "مضادات حيوية", new Guid("496edaa7-b9eb-481d-b160-c4a2e7333b87") },
                    { new Guid("c12bb473-7ef5-4d1b-aa11-ed60a12038b7"), "Painkiller", "Painkillers", "مسكنات", new Guid("496edaa7-b9eb-481d-b160-c4a2e7333b87") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaterialGroup_Code",
                table: "MaterialGroup",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaterialGroup_MTypeId",
                table: "MaterialGroup",
                column: "MTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaterialGroup");
        }
    }
}
