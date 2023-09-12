﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StorifyAPI.Migrations
{
    /// <inheritdoc />
    public partial class DropMIUIDColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MItemUnitID",
                table: "MItemUnit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MItemUnitID",
                table: "MItemUnit",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
