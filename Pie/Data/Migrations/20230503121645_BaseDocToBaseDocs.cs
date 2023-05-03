using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pie.Data.Migrations
{
    /// <inheritdoc />
    public partial class BaseDocToBaseDocs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocInBaseDocs_BaseDoc_BaseDocId",
                table: "DocInBaseDocs");

            migrationBuilder.DropForeignKey(
                name: "FK_DocOutBaseDocs_BaseDoc_BaseDocId",
                table: "DocOutBaseDocs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BaseDoc",
                table: "BaseDoc");

            migrationBuilder.RenameTable(
                name: "BaseDoc",
                newName: "BaseDocs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BaseDocs",
                table: "BaseDocs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DocInBaseDocs_BaseDocs_BaseDocId",
                table: "DocInBaseDocs",
                column: "BaseDocId",
                principalTable: "BaseDocs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DocOutBaseDocs_BaseDocs_BaseDocId",
                table: "DocOutBaseDocs",
                column: "BaseDocId",
                principalTable: "BaseDocs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocInBaseDocs_BaseDocs_BaseDocId",
                table: "DocInBaseDocs");

            migrationBuilder.DropForeignKey(
                name: "FK_DocOutBaseDocs_BaseDocs_BaseDocId",
                table: "DocOutBaseDocs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BaseDocs",
                table: "BaseDocs");

            migrationBuilder.RenameTable(
                name: "BaseDocs",
                newName: "BaseDoc");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BaseDoc",
                table: "BaseDoc",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DocInBaseDocs_BaseDoc_BaseDocId",
                table: "DocInBaseDocs",
                column: "BaseDocId",
                principalTable: "BaseDoc",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DocOutBaseDocs_BaseDoc_BaseDocId",
                table: "DocOutBaseDocs",
                column: "BaseDocId",
                principalTable: "BaseDoc",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
