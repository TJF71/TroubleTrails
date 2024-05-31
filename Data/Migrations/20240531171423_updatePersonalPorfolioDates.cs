using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace TroubleTrails.Data.Migrations
{
    public partial class updatePersonalPorfolioDates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Name",
                keyValue: "Build a Personal Porfolio",
                columns: new[] { "StartDate", "EndDate" },
                values: new object[] { new DateTime(2024, 4, 20).ToUniversalTime(), new DateTime(2024, 10, 20).ToUniversalTime() });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Name",
                keyValue: "Build a Personal Porfolio",
                columns: new[] { "StartDate", "EndDate" },
                values: new object[] { new DateTime(2024, 5, 20).ToUniversalTime(), new DateTime(2024, 9, 20).ToUniversalTime() });
        }
    }
}
