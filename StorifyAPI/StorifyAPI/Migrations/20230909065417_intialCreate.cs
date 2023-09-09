using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StorifyAPI.Migrations
{
    /// <inheritdoc />
    public partial class intialCreate : Migration
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
                name: "MGroups",
                columns: table => new
                {
                    MGroupID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    matrialTypeID = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    GlobalName = table.Column<string>(type: "NVARCHAR(200)", maxLength: 200, nullable: false),
                    LocalName = table.Column<string>(type: "NVARCHAR(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MGroups", x => x.MGroupID);
                    table.ForeignKey(
                        name: "FK_MGroups_MTypes_matrialTypeID",
                        column: x => x.matrialTypeID,
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
                    matrialGroupID = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    GlobalName = table.Column<string>(type: "NVARCHAR(200)", maxLength: 200, nullable: false),
                    LocalName = table.Column<string>(type: "NVARCHAR(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MItems", x => x.MItemID);
                    table.ForeignKey(
                        name: "FK_MItems_MGroups_matrialGroupID",
                        column: x => x.matrialGroupID,
                        principalTable: "MGroups",
                        principalColumn: "MGroupID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MGroups_Code",
                table: "MGroups",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MGroups_matrialTypeID",
                table: "MGroups",
                column: "matrialTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_MItems_Code",
                table: "MItems",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MItems_matrialGroupID",
                table: "MItems",
                column: "matrialGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_MTypes_Code",
                table: "MTypes",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MItems");

            migrationBuilder.DropTable(
                name: "MGroups");

            migrationBuilder.DropTable(
                name: "MTypes");
        }
    }
}
