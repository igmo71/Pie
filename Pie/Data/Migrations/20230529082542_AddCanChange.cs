using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pie.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCanChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CanChange",
                table: "StatusesOut",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanChange",
                table: "StatusesIn",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "StatusesIn",
                keyColumn: "Id",
                keyValue: new Guid("7f8bf9f1-92e3-4f45-84ea-461b9f82aa20"),
                column: "CanChange",
                value: false);

            migrationBuilder.UpdateData(
                table: "StatusesIn",
                keyColumn: "Id",
                keyValue: new Guid("b2cbc819-151b-489d-9b09-649aa16b2a8b"),
                column: "CanChange",
                value: false);

            migrationBuilder.UpdateData(
                table: "StatusesIn",
                keyColumn: "Id",
                keyValue: new Guid("ba575f5d-1c8d-4616-a707-1b4157746aa3"),
                column: "CanChange",
                value: false);

            migrationBuilder.UpdateData(
                table: "StatusesIn",
                keyColumn: "Id",
                keyValue: new Guid("f1cff011-6ecb-49f1-9898-2bf4a69b7b13"),
                column: "CanChange",
                value: false);

            migrationBuilder.UpdateData(
                table: "StatusesOut",
                keyColumn: "Id",
                keyValue: new Guid("17cee206-e06f-47d8-824d-14eeceaf394a"),
                column: "CanChange",
                value: false);

            migrationBuilder.UpdateData(
                table: "StatusesOut",
                keyColumn: "Id",
                keyValue: new Guid("7c2bd6be-cf81-4b1a-9acf-d4ebf416f4d3"),
                column: "CanChange",
                value: false);

            migrationBuilder.UpdateData(
                table: "StatusesOut",
                keyColumn: "Id",
                keyValue: new Guid("9eba20ce-9245-4109-92cb-a9875801fbb4"),
                column: "CanChange",
                value: false);

            migrationBuilder.UpdateData(
                table: "StatusesOut",
                keyColumn: "Id",
                keyValue: new Guid("bd1ae241-d787-4a6d-b920-029bc6577364"),
                column: "CanChange",
                value: false);

            migrationBuilder.UpdateData(
                table: "StatusesOut",
                keyColumn: "Id",
                keyValue: new Guid("c2c5935d-b332-4d84-b1fd-309ad8a65356"),
                column: "CanChange",
                value: false);

            migrationBuilder.UpdateData(
                table: "StatusesOut",
                keyColumn: "Id",
                keyValue: new Guid("e1a4c395-f7a3-40af-82ab-ad545e51eca7"),
                column: "CanChange",
                value: false);

            migrationBuilder.UpdateData(
                table: "StatusesOut",
                keyColumn: "Id",
                keyValue: new Guid("e911589b-613c-42ad-ad56-7083c481c4b4"),
                column: "CanChange",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanChange",
                table: "StatusesOut");

            migrationBuilder.DropColumn(
                name: "CanChange",
                table: "StatusesIn");
        }
    }
}
