using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StorifyAPI.Migrations
{
    /// <inheritdoc />
    public partial class MartrialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MTypes",
                columns: table => new
                {
                    MTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    GlobalName = table.Column<string>(type: "NVARCHAR(200)", maxLength: 200, nullable: false),
                    LocalName = table.Column<string>(type: "NVARCHAR(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MTypes", x => x.MTypeID);
                });

            migrationBuilder.CreateTable(
                name: "MUnits",
                columns: table => new
                {
                    MUnitID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    GlobalName = table.Column<string>(type: "NVARCHAR(200)", maxLength: 200, nullable: false),
                    LocalName = table.Column<string>(type: "NVARCHAR(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MUnits", x => x.MUnitID);
                });

            migrationBuilder.CreateTable(
                name: "MGroups",
                columns: table => new
                {
                    MGroupID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MTypeID = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    GlobalName = table.Column<string>(type: "NVARCHAR(200)", maxLength: 200, nullable: false),
                    LocalName = table.Column<string>(type: "NVARCHAR(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MGroups", x => x.MGroupID);
                    table.ForeignKey(
                        name: "FK_MGroups_MTypes_MTypeID",
                        column: x => x.MTypeID,
                        principalTable: "MTypes",
                        principalColumn: "MTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MItems",
                columns: table => new
                {
                    MItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MGroupID = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    GlobalName = table.Column<string>(type: "NVARCHAR(200)", maxLength: 200, nullable: false),
                    LocalName = table.Column<string>(type: "NVARCHAR(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MItems", x => x.MItemID);
                    table.ForeignKey(
                        name: "FK_MItems_MGroups_MGroupID",
                        column: x => x.MGroupID,
                        principalTable: "MGroups",
                        principalColumn: "MGroupID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatrialItemMatrialUnit",
                columns: table => new
                {
                    matrialItemsID = table.Column<int>(type: "int", nullable: false),
                    matrialUnitsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatrialItemMatrialUnit", x => new { x.matrialItemsID, x.matrialUnitsID });
                    table.ForeignKey(
                        name: "FK_MatrialItemMatrialUnit_MItems_matrialItemsID",
                        column: x => x.matrialItemsID,
                        principalTable: "MItems",
                        principalColumn: "MItemID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatrialItemMatrialUnit_MUnits_matrialUnitsID",
                        column: x => x.matrialUnitsID,
                        principalTable: "MUnits",
                        principalColumn: "MUnitID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MItemUnit",
                columns: table => new
                {
                    MItemID = table.Column<int>(type: "int", nullable: false),
                    MUnitID = table.Column<int>(type: "int", nullable: false),
                    MItemUnitID = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "Money", nullable: false),
                    CFactor = table.Column<int>(type: "int", nullable: false),
                    MCUnitID = table.Column<int>(type: "int", nullable: false),
                    isMain = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MItemUnit", x => new { x.MUnitID, x.MItemID });
                    table.ForeignKey(
                        name: "FK_MItemUnit_MItems_MItemID",
                        column: x => x.MItemID,
                        principalTable: "MItems",
                        principalColumn: "MItemID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MItemUnit_MUnits_MCUnitID",
                        column: x => x.MCUnitID,
                        principalTable: "MUnits",
                        principalColumn: "MUnitID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MItemUnit_MUnits_MUnitID",
                        column: x => x.MUnitID,
                        principalTable: "MUnits",
                        principalColumn: "MUnitID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MatrialItemMatrialUnit_matrialUnitsID",
                table: "MatrialItemMatrialUnit",
                column: "matrialUnitsID");

            migrationBuilder.CreateIndex(
                name: "IX_MGroups_Code",
                table: "MGroups",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MGroups_MTypeID",
                table: "MGroups",
                column: "MTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_MItems_Code",
                table: "MItems",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MItems_MGroupID",
                table: "MItems",
                column: "MGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_MItemUnit_MCUnitID",
                table: "MItemUnit",
                column: "MCUnitID");

            migrationBuilder.CreateIndex(
                name: "IX_MItemUnit_MItemID",
                table: "MItemUnit",
                column: "MItemID");

            migrationBuilder.CreateIndex(
                name: "IX_MTypes_Code",
                table: "MTypes",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MUnits_Code",
                table: "MUnits",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatrialItemMatrialUnit");

            migrationBuilder.DropTable(
                name: "MItemUnit");

            migrationBuilder.DropTable(
                name: "MItems");

            migrationBuilder.DropTable(
                name: "MUnits");

            migrationBuilder.DropTable(
                name: "MGroups");

            migrationBuilder.DropTable(
                name: "MTypes");
        }
    }
}
