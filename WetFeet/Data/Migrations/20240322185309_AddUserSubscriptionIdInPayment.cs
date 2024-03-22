using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserSubscriptionIdInPayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "210a0d41-eac3-4bb2-bb89-0da46bc04c52");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b746ed4b-c759-47a7-8239-95971b7fec27");

            migrationBuilder.DeleteData(
                table: "SubscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("d100c7cc-0996-4dd4-b22d-592e66fc4ff1"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserSubscriptionId",
                table: "Payments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "44f31f8a-765e-495a-8efa-52937683a671", "3", "Audience", "Audience" },
                    { "ee720c2c-6fea-474f-b7cb-2c6bfd6fe146", "2", "Creator", "Creator" }
                });

            migrationBuilder.InsertData(
                table: "SubscriptionPlans",
                columns: new[] { "Id", "Details", "IsActive", "MonthlyAmount", "YearlyAmount" },
                values: new object[] { new Guid("14c5ffe7-ca6b-49ed-990d-c55099b0a3c3"), "Basic plan", true, 3.9900000000000002, 39.990000000000002 });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_UserSubscriptionId",
                table: "Payments",
                column: "UserSubscriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_UserSubscriptionPlans_UserSubscriptionId",
                table: "Payments",
                column: "UserSubscriptionId",
                principalTable: "UserSubscriptionPlans",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_UserSubscriptionPlans_UserSubscriptionId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_UserSubscriptionId",
                table: "Payments");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "44f31f8a-765e-495a-8efa-52937683a671");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ee720c2c-6fea-474f-b7cb-2c6bfd6fe146");

            migrationBuilder.DeleteData(
                table: "SubscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("14c5ffe7-ca6b-49ed-990d-c55099b0a3c3"));

            migrationBuilder.DropColumn(
                name: "UserSubscriptionId",
                table: "Payments");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "210a0d41-eac3-4bb2-bb89-0da46bc04c52", "3", "Audience", "Audience" },
                    { "b746ed4b-c759-47a7-8239-95971b7fec27", "2", "Creator", "Creator" }
                });

            migrationBuilder.InsertData(
                table: "SubscriptionPlans",
                columns: new[] { "Id", "Details", "IsActive", "MonthlyAmount", "YearlyAmount" },
                values: new object[] { new Guid("d100c7cc-0996-4dd4-b22d-592e66fc4ff1"), "Basic plan", true, 3.9900000000000002, 39.990000000000002 });
        }
    }
}
