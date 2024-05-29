using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace TroubleTrails.Data.Migrations
{
    public partial class UpdatePortfolioDatesCorrected : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Name",
                keyValue: "Build a Personal Porfolio",
                columns: new[] { "StartDate", "EndDate" },
                values: new object[]
                {
                    DateTime.SpecifyKind(new DateTime(2024, 5, 20), DateTimeKind.Utc),
                    DateTime.SpecifyKind(new DateTime(2024, 5, 20).AddMonths(1), DateTimeKind.Utc)
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Name",
                keyValue: "Build a Personal Porfolio",
                columns: new[] { "StartDate", "EndDate" },
                values: new object[]
                {
                    DateTime.SpecifyKind(new DateTime(2021, 8, 20), DateTimeKind.Utc),
                    DateTime.SpecifyKind(new DateTime(2021, 8, 20).AddMonths(1), DateTimeKind.Utc)
                });
        }
    }
}
