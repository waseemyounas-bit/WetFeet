using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class userauth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2fc8858d-5faf-4182-b919-4d8bfaf23800");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b2b70a15-002d-471e-a6ae-836e02f5ab28");

            migrationBuilder.AddColumn<string>(
                name: "About",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7571590f-5fa2-4d11-979b-7cc5ab4910d7", "2", "Creator", "Creator" },
                    { "e24de974-b223-4a8a-84e8-08120eb30942", "3", "Audience", "Audience" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7571590f-5fa2-4d11-979b-7cc5ab4910d7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e24de974-b223-4a8a-84e8-08120eb30942");

            migrationBuilder.DropColumn(
                name: "About",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2fc8858d-5faf-4182-b919-4d8bfaf23800", "3", "Audience", "Audience" },
                    { "b2b70a15-002d-471e-a6ae-836e02f5ab28", "2", "Creator", "Creator" }
                });
        }
    }
}
