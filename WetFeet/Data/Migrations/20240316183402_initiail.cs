using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class initiail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "07893eac-87d7-48fb-8f02-006fc2e7fcd9", "3", "Audience", "Audience" },
                    { "50874ac7-36e7-46fd-b31e-a2a62a6824f9", "2", "Creator", "Creator" }
                });

            migrationBuilder.InsertData(
                table: "SubscriptionPlans",
                columns: new[] { "Id", "Details", "IsActive", "MonthlyAmount", "YearlyAmount" },
                values: new object[] { new Guid("1f728658-0374-47d0-8473-444f2c20259d"), "Basic plan", true, 3.9900000000000002, 39.990000000000002 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "07893eac-87d7-48fb-8f02-006fc2e7fcd9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "50874ac7-36e7-46fd-b31e-a2a62a6824f9");

            migrationBuilder.DeleteData(
                table: "SubscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("1f728658-0374-47d0-8473-444f2c20259d"));
        }
    }
}
