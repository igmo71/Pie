using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pie.Data.Migrations
{
    /// <inheritdoc />
    public partial class Remove_Active : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Partners");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "BaseDocs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Partners",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Managers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "BaseDocs",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
