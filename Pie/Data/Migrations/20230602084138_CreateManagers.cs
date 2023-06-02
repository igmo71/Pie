using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pie.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateManagers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocsIn_Warehouses_TransferWarehouseId",
                table: "DocsIn");

            migrationBuilder.DropForeignKey(
                name: "FK_DocsOut_Warehouses_TransferWarehouseId",
                table: "DocsOut");

            migrationBuilder.RenameColumn(
                name: "TransferWarehouseId",
                table: "DocsOut",
                newName: "ManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_DocsOut_TransferWarehouseId",
                table: "DocsOut",
                newName: "IX_DocsOut_ManagerId");

            migrationBuilder.RenameColumn(
                name: "TransferWarehouseId",
                table: "DocsIn",
                newName: "ManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_DocsIn_TransferWarehouseId",
                table: "DocsIn",
                newName: "IX_DocsIn_ManagerId");

            migrationBuilder.CreateTable(
                name: "Managers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_DocsIn_Managers_ManagerId",
                table: "DocsIn",
                column: "ManagerId",
                principalTable: "Managers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_DocsOut_Managers_ManagerId",
                table: "DocsOut",
                column: "ManagerId",
                principalTable: "Managers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocsIn_Managers_ManagerId",
                table: "DocsIn");

            migrationBuilder.DropForeignKey(
                name: "FK_DocsOut_Managers_ManagerId",
                table: "DocsOut");

            migrationBuilder.DropTable(
                name: "Managers");

            migrationBuilder.RenameColumn(
                name: "ManagerId",
                table: "DocsOut",
                newName: "TransferWarehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_DocsOut_ManagerId",
                table: "DocsOut",
                newName: "IX_DocsOut_TransferWarehouseId");

            migrationBuilder.RenameColumn(
                name: "ManagerId",
                table: "DocsIn",
                newName: "TransferWarehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_DocsIn_ManagerId",
                table: "DocsIn",
                newName: "IX_DocsIn_TransferWarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_DocsIn_Warehouses_TransferWarehouseId",
                table: "DocsIn",
                column: "TransferWarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DocsOut_Warehouses_TransferWarehouseId",
                table: "DocsOut",
                column: "TransferWarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id");
        }
    }
}
