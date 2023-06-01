using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pie.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateDeliveryAreas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeliveryAddress",
                table: "DocsOut",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeliveryAreaId",
                table: "DocsOut",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsTransfer",
                table: "DocsOut",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "TransferWarehouseId",
                table: "DocsOut",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsTransfer",
                table: "DocsIn",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "TransferWarehouseId",
                table: "DocsIn",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DeliveryAreas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsFolder = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryAreas", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocsOut_DeliveryAreaId",
                table: "DocsOut",
                column: "DeliveryAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_DocsOut_TransferWarehouseId",
                table: "DocsOut",
                column: "TransferWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_DocsIn_TransferWarehouseId",
                table: "DocsIn",
                column: "TransferWarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_DocsIn_Warehouses_TransferWarehouseId",
                table: "DocsIn",
                column: "TransferWarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DocsOut_DeliveryAreas_DeliveryAreaId",
                table: "DocsOut",
                column: "DeliveryAreaId",
                principalTable: "DeliveryAreas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DocsOut_Warehouses_TransferWarehouseId",
                table: "DocsOut",
                column: "TransferWarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocsIn_Warehouses_TransferWarehouseId",
                table: "DocsIn");

            migrationBuilder.DropForeignKey(
                name: "FK_DocsOut_DeliveryAreas_DeliveryAreaId",
                table: "DocsOut");

            migrationBuilder.DropForeignKey(
                name: "FK_DocsOut_Warehouses_TransferWarehouseId",
                table: "DocsOut");

            migrationBuilder.DropTable(
                name: "DeliveryAreas");

            migrationBuilder.DropIndex(
                name: "IX_DocsOut_DeliveryAreaId",
                table: "DocsOut");

            migrationBuilder.DropIndex(
                name: "IX_DocsOut_TransferWarehouseId",
                table: "DocsOut");

            migrationBuilder.DropIndex(
                name: "IX_DocsIn_TransferWarehouseId",
                table: "DocsIn");

            migrationBuilder.DropColumn(
                name: "DeliveryAddress",
                table: "DocsOut");

            migrationBuilder.DropColumn(
                name: "DeliveryAreaId",
                table: "DocsOut");

            migrationBuilder.DropColumn(
                name: "IsTransfer",
                table: "DocsOut");

            migrationBuilder.DropColumn(
                name: "TransferWarehouseId",
                table: "DocsOut");

            migrationBuilder.DropColumn(
                name: "IsTransfer",
                table: "DocsIn");

            migrationBuilder.DropColumn(
                name: "TransferWarehouseId",
                table: "DocsIn");
        }
    }
}
