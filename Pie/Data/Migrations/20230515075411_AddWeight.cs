using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pie.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddWeight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConcreteTime",
                table: "QueuesIn");

            migrationBuilder.DropColumn(
                name: "Days",
                table: "QueuesIn");

            migrationBuilder.DropColumn(
                name: "Hours",
                table: "QueuesIn");

            migrationBuilder.DropColumn(
                name: "Minutes",
                table: "QueuesIn");

            migrationBuilder.AddColumn<float>(
                name: "Weight",
                table: "DocOutProducts",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Weight",
                table: "DocInProducts",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Weight",
                table: "DocOutProducts");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "DocInProducts");

            migrationBuilder.AddColumn<TimeOnly>(
                name: "ConcreteTime",
                table: "QueuesIn",
                type: "time without time zone",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<int>(
                name: "Days",
                table: "QueuesIn",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Hours",
                table: "QueuesIn",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Minutes",
                table: "QueuesIn",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
