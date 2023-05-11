using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pie.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateQueueNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QueueNumber",
                columns: table => new
                {
                    Value = table.Column<string>(type: "text", nullable: false),
                    CharValue = table.Column<int>(type: "integer", nullable: false),
                    NumValue = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QueueNumber", x => x.Value);
                });

            migrationBuilder.InsertData(
                table: "QueueNumber",
                columns: new[] { "Value", "CharValue", "NumValue" },
                values: new object[] { "A000", 0, 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QueueNumber");
        }
    }
}
