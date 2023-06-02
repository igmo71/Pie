using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pie.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_Unit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "DocOutProducts",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "DocInProducts",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unit",
                table: "DocOutProducts");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "DocInProducts");
        }
    }
}
