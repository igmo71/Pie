using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pie.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateBasicModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BaseDocs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseDocs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QueuesIn",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Key = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QueuesIn", x => x.Id);
                    table.UniqueConstraint("AK_QueuesIn_Key", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "QueuesOut",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Key = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QueuesOut", x => x.Id);
                    table.UniqueConstraint("AK_QueuesOut_Key", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "StatusesIn",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Key = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusesIn", x => x.Id);
                    table.UniqueConstraint("AK_StatusesIn_Key", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "StatusesOut",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Key = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusesOut", x => x.Id);
                    table.UniqueConstraint("AK_StatusesOut_Key", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocsIn",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusKey = table.Column<int>(type: "int", nullable: true),
                    QueueKey = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Number = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocsIn", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocsIn_QueuesIn_QueueKey",
                        column: x => x.QueueKey,
                        principalTable: "QueuesIn",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_DocsIn_StatusesIn_StatusKey",
                        column: x => x.StatusKey,
                        principalTable: "StatusesIn",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_DocsIn_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "DocsOut",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusKey = table.Column<int>(type: "int", nullable: true),
                    QueueKey = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Number = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocsOut", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocsOut_QueuesOut_QueueKey",
                        column: x => x.QueueKey,
                        principalTable: "QueuesOut",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_DocsOut_StatusesOut_StatusKey",
                        column: x => x.StatusKey,
                        principalTable: "StatusesOut",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_DocsOut_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "DocInBaseDocs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocInId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BaseDocId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocInBaseDocs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocInBaseDocs_BaseDocs_BaseDocId",
                        column: x => x.BaseDocId,
                        principalTable: "BaseDocs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocInBaseDocs_DocsIn_DocInId",
                        column: x => x.DocInId,
                        principalTable: "DocsIn",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocInProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocInId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Count = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocInProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocInProducts_DocsIn_DocInId",
                        column: x => x.DocInId,
                        principalTable: "DocsIn",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocInProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocOutBaseDocs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocOutId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BaseDocId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocOutBaseDocs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocOutBaseDocs_BaseDocs_BaseDocId",
                        column: x => x.BaseDocId,
                        principalTable: "BaseDocs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocOutBaseDocs_DocsOut_DocOutId",
                        column: x => x.DocOutId,
                        principalTable: "DocsOut",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocOutProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocOutId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Count = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocOutProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocOutProducts_DocsOut_DocOutId",
                        column: x => x.DocOutId,
                        principalTable: "DocsOut",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocOutProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocInBaseDocs_BaseDocId",
                table: "DocInBaseDocs",
                column: "BaseDocId");

            migrationBuilder.CreateIndex(
                name: "IX_DocInBaseDocs_DocInId",
                table: "DocInBaseDocs",
                column: "DocInId");

            migrationBuilder.CreateIndex(
                name: "IX_DocInProducts_DocInId",
                table: "DocInProducts",
                column: "DocInId");

            migrationBuilder.CreateIndex(
                name: "IX_DocInProducts_ProductId",
                table: "DocInProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_DocOutBaseDocs_BaseDocId",
                table: "DocOutBaseDocs",
                column: "BaseDocId");

            migrationBuilder.CreateIndex(
                name: "IX_DocOutBaseDocs_DocOutId",
                table: "DocOutBaseDocs",
                column: "DocOutId");

            migrationBuilder.CreateIndex(
                name: "IX_DocOutProducts_DocOutId",
                table: "DocOutProducts",
                column: "DocOutId");

            migrationBuilder.CreateIndex(
                name: "IX_DocOutProducts_ProductId",
                table: "DocOutProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_DocsIn_QueueKey",
                table: "DocsIn",
                column: "QueueKey");

            migrationBuilder.CreateIndex(
                name: "IX_DocsIn_StatusKey",
                table: "DocsIn",
                column: "StatusKey");

            migrationBuilder.CreateIndex(
                name: "IX_DocsIn_WarehouseId",
                table: "DocsIn",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_DocsOut_QueueKey",
                table: "DocsOut",
                column: "QueueKey");

            migrationBuilder.CreateIndex(
                name: "IX_DocsOut_StatusKey",
                table: "DocsOut",
                column: "StatusKey");

            migrationBuilder.CreateIndex(
                name: "IX_DocsOut_WarehouseId",
                table: "DocsOut",
                column: "WarehouseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocInBaseDocs");

            migrationBuilder.DropTable(
                name: "DocInProducts");

            migrationBuilder.DropTable(
                name: "DocOutBaseDocs");

            migrationBuilder.DropTable(
                name: "DocOutProducts");

            migrationBuilder.DropTable(
                name: "DocsIn");

            migrationBuilder.DropTable(
                name: "BaseDocs");

            migrationBuilder.DropTable(
                name: "DocsOut");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "QueuesIn");

            migrationBuilder.DropTable(
                name: "StatusesIn");

            migrationBuilder.DropTable(
                name: "QueuesOut");

            migrationBuilder.DropTable(
                name: "StatusesOut");

            migrationBuilder.DropTable(
                name: "Warehouses");
        }
    }
}
