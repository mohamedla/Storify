using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StorifyAPI.Migrations.Repository
{
    /// <inheritdoc />
    public partial class InitDataForStoreandEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Stores",
                columns: new[] { "StoreID", "StoreCode", "CostCenterID", "StoreGName", "StoreLName" },
                values: new object[] { new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), "MainStore", null, "Main Matrial Store", "مخزن العناصر الرئيسى" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "Age", "EmployeeCode", "Name", "StoreId" },
                values: new object[] { new Guid("f9e4c537-87d6-810f-9a73-2754e9111870"), 24, "299159", "Mohamed Ashraf", new Guid("80abbca8-664d-4b20-b5de-024705497d4a") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("f9e4c537-87d6-810f-9a73-2754e9111870"));

            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "StoreID",
                keyValue: new Guid("80abbca8-664d-4b20-b5de-024705497d4a"));
        }
    }
}
