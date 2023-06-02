using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Pie.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreatePartners : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PartnerId",
                table: "DocsOut",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PartnerId",
                table: "DocsIn",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Partners",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partners", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9423e7b8-b496-41e8-b9c9-416b74823db9",
                column: "Name",
                value: "Manager");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "22919707-7d2c-450d-92e7-19f36935bcdb",
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName" },
                values: new object[] { "18c24c93-8d87-4450-9500-32f059f6398a", "Игорь", "Могильницкий" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "WarehouseId" },
                values: new object[] { "de3f6ced-85ca-4feb-8d98-395dc8ee71cb", 0, "3bd6e424-97b8-4f30-9f50-44e9368ae7c5", "admin@www", true, "Админ", null, true, null, "ADMIN@WWW", "ADMIN@WWW", "AQAAAAIAAYagAAAAELfYeQJOlhGQE7zLxj69o4waFM3ISF424TUMU9/DMkWPr03XyfD7EJO3XUlOkKOd0A==", null, false, "I5VGBC5NXRMCMKKBSXQIJNJC6UFZUTMF", false, "admin@www", null });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "049c2135-b769-4ea5-986a-a5231330fe46", "de3f6ced-85ca-4feb-8d98-395dc8ee71cb" },
                    { "9423e7b8-b496-41e8-b9c9-416b74823db9", "de3f6ced-85ca-4feb-8d98-395dc8ee71cb" },
                    { "d6bfb7c2-9a45-45e5-b27a-3b7cba85527f", "de3f6ced-85ca-4feb-8d98-395dc8ee71cb" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocsOut_PartnerId",
                table: "DocsOut",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_DocsIn_PartnerId",
                table: "DocsIn",
                column: "PartnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_DocsIn_Partners_PartnerId",
                table: "DocsIn",
                column: "PartnerId",
                principalTable: "Partners",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_DocsOut_Partners_PartnerId",
                table: "DocsOut",
                column: "PartnerId",
                principalTable: "Partners",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocsIn_Partners_PartnerId",
                table: "DocsIn");

            migrationBuilder.DropForeignKey(
                name: "FK_DocsOut_Partners_PartnerId",
                table: "DocsOut");

            migrationBuilder.DropTable(
                name: "Partners");

            migrationBuilder.DropIndex(
                name: "IX_DocsOut_PartnerId",
                table: "DocsOut");

            migrationBuilder.DropIndex(
                name: "IX_DocsIn_PartnerId",
                table: "DocsIn");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "049c2135-b769-4ea5-986a-a5231330fe46", "de3f6ced-85ca-4feb-8d98-395dc8ee71cb" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "9423e7b8-b496-41e8-b9c9-416b74823db9", "de3f6ced-85ca-4feb-8d98-395dc8ee71cb" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "d6bfb7c2-9a45-45e5-b27a-3b7cba85527f", "de3f6ced-85ca-4feb-8d98-395dc8ee71cb" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "de3f6ced-85ca-4feb-8d98-395dc8ee71cb");

            migrationBuilder.DropColumn(
                name: "PartnerId",
                table: "DocsOut");

            migrationBuilder.DropColumn(
                name: "PartnerId",
                table: "DocsIn");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9423e7b8-b496-41e8-b9c9-416b74823db9",
                column: "Name",
                value: "User");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "22919707-7d2c-450d-92e7-19f36935bcdb",
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName" },
                values: new object[] { "2b68aa3c-d884-475f-8a7a-f72d5666f9ae", "Админ", null });
        }
    }
}
