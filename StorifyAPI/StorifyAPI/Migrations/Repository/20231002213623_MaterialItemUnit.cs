using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StorifyAPI.Migrations.Repository
{
    /// <inheritdoc />
    public partial class MaterialItemUnit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MaterialItemUnit",
                columns: table => new
                {
                    MItemUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "Money", nullable: false),
                    AveragePrice = table.Column<decimal>(type: "Money", nullable: false),
                    LastPrice = table.Column<decimal>(type: "Money", nullable: false),
                    CFactor = table.Column<int>(type: "int", nullable: false),
                    IsMain = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialItemUnit", x => x.MItemUnitId);
                    table.ForeignKey(
                        name: "FK_MaterialItemUnit_MaterialItem_MItemId",
                        column: x => x.MItemId,
                        principalTable: "MaterialItem",
                        principalColumn: "MItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialItemUnit_MaterialUnit_MUnitId",
                        column: x => x.MUnitId,
                        principalTable: "MaterialUnit",
                        principalColumn: "MUnitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "MaterialItemUnit",
                columns: new[] { "MItemUnitId", "AveragePrice", "CFactor", "IsMain", "MItemId", "LastPrice", "MUnitId", "UnitPrice" },
                values: new object[,]
                {
                    { new Guid("496edaa7-98ce-e45d-90ae-c4a2e7333b87"), 5m, 7, true, new Guid("496edaa7-2a6e-481d-abcd-c4a2e7333b87"), 6m, new Guid("c12bb473-adfe-4d1b-2245-ed60a12038b7"), 12.5m },
                    { new Guid("496edaa7-98ce-e45d-a34d-c4a2e7333b87"), 5m, 7, false, new Guid("496edaa7-2a6e-481d-abcd-c4a2e7333b87"), 6m, new Guid("496edaa7-76ea-481d-abcd-c4a2e7333b87"), 12.5m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaterialItemUnit_MItemId_MUnitId",
                table: "MaterialItemUnit",
                columns: new[] { "MItemId", "MUnitId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaterialItemUnit");
        }
    }
}
