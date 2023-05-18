using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Pie.Data.Migrations
{
    /// <inheritdoc />
    public partial class Refactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "049c2135-b769-4ea5-986a-a5231330fe46", null, "Service1c", "SERVICE1C" },
                    { "9423e7b8-b496-41e8-b9c9-416b74823db9", null, "User", "USER" },
                    { "d6bfb7c2-9a45-45e5-b27a-3b7cba85527f", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "WarehouseId" },
                values: new object[,]
                {
                    { "0b60c927-0e9f-4fa7-8422-e0e16e6fa5f4", 0, "655eae0d-3ab4-4688-87ec-700d6c6c9567", "Service1c@www", true, "Service1c", null, true, null, "SERVICE1C@WWW", "SERVICE1C@WWW", "AQAAAAIAAYagAAAAEAOaCAADpFz/XmkyKhEkd0FjnlPHtkUSiV7GdH10SvSKvK4eZhtJHFnWl66jxbydXw==", null, false, "JCSGTYDEWVEPHPV7DDVTSY263NY4JDBP", false, "Service1c@www", null },
                    { "22919707-7d2c-450d-92e7-19f36935bcdb", 0, "2b68aa3c-d884-475f-8a7a-f72d5666f9ae", "igmo@dobroga.ru", true, "Админ", null, true, null, "IGMO@DOBROGA.RU", "IGMO@DOBROGA.RU", "AQAAAAIAAYagAAAAEDgydLmvi4/0kDXZB6+ShJFMNIK8Xzgaawytbvp8IMJquSZ/4hO8sPu9mlXC5uS9IQ==", null, false, "HCJOWYFSM63CJOZM5AZAGXSHEI257BCI", false, "igmo@dobroga.ru", null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "049c2135-b769-4ea5-986a-a5231330fe46", "0b60c927-0e9f-4fa7-8422-e0e16e6fa5f4" },
                    { "049c2135-b769-4ea5-986a-a5231330fe46", "22919707-7d2c-450d-92e7-19f36935bcdb" },
                    { "9423e7b8-b496-41e8-b9c9-416b74823db9", "22919707-7d2c-450d-92e7-19f36935bcdb" },
                    { "d6bfb7c2-9a45-45e5-b27a-3b7cba85527f", "22919707-7d2c-450d-92e7-19f36935bcdb" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "049c2135-b769-4ea5-986a-a5231330fe46", "0b60c927-0e9f-4fa7-8422-e0e16e6fa5f4" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "049c2135-b769-4ea5-986a-a5231330fe46", "22919707-7d2c-450d-92e7-19f36935bcdb" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "9423e7b8-b496-41e8-b9c9-416b74823db9", "22919707-7d2c-450d-92e7-19f36935bcdb" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "d6bfb7c2-9a45-45e5-b27a-3b7cba85527f", "22919707-7d2c-450d-92e7-19f36935bcdb" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "049c2135-b769-4ea5-986a-a5231330fe46");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9423e7b8-b496-41e8-b9c9-416b74823db9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d6bfb7c2-9a45-45e5-b27a-3b7cba85527f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0b60c927-0e9f-4fa7-8422-e0e16e6fa5f4");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "22919707-7d2c-450d-92e7-19f36935bcdb");
        }
    }
}
