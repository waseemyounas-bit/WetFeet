using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddContentFilesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9297b966-ebd4-4437-a676-664399154daa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cf8c36bf-e3cc-427f-b817-90f58a46cbec");

            migrationBuilder.DeleteData(
                table: "SubscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("05a0f457-ba5a-4037-aa46-3344bc3f9246"));

            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Contents");

            migrationBuilder.CreateTable(
                name: "ContentFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContentFiles_Contents_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Contents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6fdd3547-1bab-4bf6-9704-ddbf0ba92c85", "2", "Creator", "Creator" },
                    { "ff4b781c-dead-4a12-a0d3-2c97d9625812", "3", "Audience", "Audience" }
                });

            migrationBuilder.InsertData(
                table: "SubscriptionPlans",
                columns: new[] { "Id", "Details", "IsActive", "MonthlyAmount", "YearlyAmount" },
                values: new object[] { new Guid("7dfa5ace-2790-4132-9f5b-06754a1a3411"), "Basic plan", true, 3.9900000000000002, 39.990000000000002 });

            migrationBuilder.CreateIndex(
                name: "IX_ContentFiles_ContentId",
                table: "ContentFiles",
                column: "ContentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContentFiles");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6fdd3547-1bab-4bf6-9704-ddbf0ba92c85");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ff4b781c-dead-4a12-a0d3-2c97d9625812");

            migrationBuilder.DeleteData(
                table: "SubscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("7dfa5ace-2790-4132-9f5b-06754a1a3411"));

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "Contents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Contents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Contents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9297b966-ebd4-4437-a676-664399154daa", "2", "Creator", "Creator" },
                    { "cf8c36bf-e3cc-427f-b817-90f58a46cbec", "3", "Audience", "Audience" }
                });

            migrationBuilder.InsertData(
                table: "SubscriptionPlans",
                columns: new[] { "Id", "Details", "IsActive", "MonthlyAmount", "YearlyAmount" },
                values: new object[] { new Guid("05a0f457-ba5a-4037-aa46-3344bc3f9246"), "Basic plan", true, 3.9900000000000002, 39.990000000000002 });
        }
    }
}
