using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Reminde_API.Migrations
{
    /// <inheritdoc />
    public partial class RolesDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bbcc9d02-f8e4-42c7-b5e7-79276e134e8d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f51c2f37-9b32-43eb-bfde-309a95f80089");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a70f576d-b641-48ec-ad72-9da2ff23189e", null, "Admin", "ADMIN" },
                    { "cef40592-c593-45a7-8156-999d7d010988", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a70f576d-b641-48ec-ad72-9da2ff23189e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cef40592-c593-45a7-8156-999d7d010988");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "bbcc9d02-f8e4-42c7-b5e7-79276e134e8d", null, "User", "USER" },
                    { "f51c2f37-9b32-43eb-bfde-309a95f80089", null, "Admin", "ADMIN" }
                });
        }
    }
}
