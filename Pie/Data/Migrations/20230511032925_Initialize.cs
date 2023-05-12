﻿using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Pie.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BaseDocs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseDocs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChangeReasons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Discriminator = table.Column<string>(type: "character varying(21)", maxLength: 21, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeReasons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QueuesIn",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Key = table.Column<int>(type: "integer", nullable: false),
                    Days = table.Column<int>(type: "integer", nullable: false),
                    Hours = table.Column<int>(type: "integer", nullable: false),
                    Minutes = table.Column<int>(type: "integer", nullable: false),
                    ConcreteTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Key = table.Column<int>(type: "integer", nullable: false),
                    Days = table.Column<int>(type: "integer", nullable: false),
                    Hours = table.Column<int>(type: "integer", nullable: false),
                    Minutes = table.Column<int>(type: "integer", nullable: false),
                    ConcreteTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Key = table.Column<int>(type: "integer", nullable: false)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Key = table.Column<int>(type: "integer", nullable: false)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocsIn",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StatusKey = table.Column<int>(type: "integer", nullable: true),
                    QueueKey = table.Column<int>(type: "integer", nullable: true),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    DateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uuid", nullable: true),
                    Comment = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StatusKey = table.Column<int>(type: "integer", nullable: true),
                    QueueKey = table.Column<int>(type: "integer", nullable: true),
                    QueueNumber = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    ShipDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    DateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uuid", nullable: true),
                    Comment = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocId = table.Column<Guid>(type: "uuid", nullable: false),
                    BaseDocId = table.Column<Guid>(type: "uuid", nullable: false)
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
                        name: "FK_DocInBaseDocs_DocsIn_DocId",
                        column: x => x.DocId,
                        principalTable: "DocsIn",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocInProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    CountPlan = table.Column<float>(type: "real", nullable: false),
                    CountFact = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocInProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocInProducts_DocsIn_DocId",
                        column: x => x.DocId,
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
                name: "DocInProductsHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChangeReasonId = table.Column<Guid>(type: "uuid", nullable: true),
                    DateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UserId = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: true),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    CountPlan = table.Column<float>(type: "real", nullable: false),
                    CountFact = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocInProductsHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocInProductsHistory_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_DocInProductsHistory_ChangeReasons_ChangeReasonId",
                        column: x => x.ChangeReasonId,
                        principalTable: "ChangeReasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_DocInProductsHistory_DocsIn_DocId",
                        column: x => x.DocId,
                        principalTable: "DocsIn",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocInProductsHistory_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocsInHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocId = table.Column<Guid>(type: "uuid", nullable: false),
                    StatusKey = table.Column<int>(type: "integer", nullable: true),
                    DateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UserId = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocsInHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocsInHistory_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_DocsInHistory_DocsIn_DocId",
                        column: x => x.DocId,
                        principalTable: "DocsIn",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocsInHistory_StatusesIn_StatusKey",
                        column: x => x.StatusKey,
                        principalTable: "StatusesIn",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "DocOutBaseDocs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocId = table.Column<Guid>(type: "uuid", nullable: false),
                    BaseDocId = table.Column<Guid>(type: "uuid", nullable: false)
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
                        name: "FK_DocOutBaseDocs_DocsOut_DocId",
                        column: x => x.DocId,
                        principalTable: "DocsOut",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocOutProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    CountPlan = table.Column<float>(type: "real", nullable: false),
                    CountFact = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocOutProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocOutProducts_DocsOut_DocId",
                        column: x => x.DocId,
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

            migrationBuilder.CreateTable(
                name: "DocOutProductsHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChangeReasonId = table.Column<Guid>(type: "uuid", nullable: true),
                    DateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UserId = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: true),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    CountPlan = table.Column<float>(type: "real", nullable: false),
                    CountFact = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocOutProductsHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocOutProductsHistory_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_DocOutProductsHistory_ChangeReasons_ChangeReasonId",
                        column: x => x.ChangeReasonId,
                        principalTable: "ChangeReasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_DocOutProductsHistory_DocsOut_DocId",
                        column: x => x.DocId,
                        principalTable: "DocsOut",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocOutProductsHistory_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocsOutHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocId = table.Column<Guid>(type: "uuid", nullable: false),
                    StatusKey = table.Column<int>(type: "integer", nullable: true),
                    DateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UserId = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocsOutHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocsOutHistory_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_DocsOutHistory_DocsOut_DocId",
                        column: x => x.DocId,
                        principalTable: "DocsOut",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocsOutHistory_StatusesOut_StatusKey",
                        column: x => x.StatusKey,
                        principalTable: "StatusesOut",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                table: "QueuesOut",
                columns: new[] { "Id", "Active", "ConcreteTime", "Days", "Hours", "Key", "Minutes", "Name" },
                values: new object[,]
                {
                    { new Guid("3558d2ba-ffb6-4f08-9891-f7f1e8853c83"), true, new TimeOnly(0, 0, 0), 0, 0, 20, 0, "Собрать к дате" },
                    { new Guid("7e83260a-316f-4a1f-be9a-bf353b118536"), true, new TimeOnly(0, 0, 0), 0, 0, 10, 0, "Живая очередь" },
                    { new Guid("8bdc656e-8a2c-4aef-9422-e0a419608190"), true, new TimeOnly(0, 0, 0), 0, 0, 40, 0, "Очередность не указана" },
                    { new Guid("d964fcad-d71d-480a-bdeb-0b2c045fcd90"), true, new TimeOnly(0, 0, 0), 0, 0, 30, 0, "Собственная доставка" }
                });

            migrationBuilder.InsertData(
                table: "StatusesOut",
                columns: new[] { "Id", "Active", "Key", "Name" },
                values: new object[,]
                {
                    { new Guid("17cee206-e06f-47d8-824d-14eeceaf394a"), false, 3, "ВПроцессеПроверки" },
                    { new Guid("7c2bd6be-cf81-4b1a-9acf-d4ebf416f4d3"), true, 5, "КОтгрузке" },
                    { new Guid("9eba20ce-9245-4109-92cb-a9875801fbb4"), true, 6, "Отгружен" },
                    { new Guid("bd1ae241-d787-4a6d-b920-029bc6577364"), false, 2, "КПроверке" },
                    { new Guid("c2c5935d-b332-4d84-b1fd-309ad8a65356"), true, 0, "Подготовлено" },
                    { new Guid("e1a4c395-f7a3-40af-82ab-ad545e51eca7"), true, 1, "КОтбору" },
                    { new Guid("e911589b-613c-42ad-ad56-7083c481c4b4"), false, 4, "Проверен" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocInBaseDocs_BaseDocId",
                table: "DocInBaseDocs",
                column: "BaseDocId");

            migrationBuilder.CreateIndex(
                name: "IX_DocInBaseDocs_DocId",
                table: "DocInBaseDocs",
                column: "DocId");

            migrationBuilder.CreateIndex(
                name: "IX_DocInProducts_DocId",
                table: "DocInProducts",
                column: "DocId");

            migrationBuilder.CreateIndex(
                name: "IX_DocInProducts_ProductId",
                table: "DocInProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_DocInProductsHistory_ChangeReasonId",
                table: "DocInProductsHistory",
                column: "ChangeReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_DocInProductsHistory_DocId",
                table: "DocInProductsHistory",
                column: "DocId");

            migrationBuilder.CreateIndex(
                name: "IX_DocInProductsHistory_ProductId",
                table: "DocInProductsHistory",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_DocInProductsHistory_UserId",
                table: "DocInProductsHistory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DocOutBaseDocs_BaseDocId",
                table: "DocOutBaseDocs",
                column: "BaseDocId");

            migrationBuilder.CreateIndex(
                name: "IX_DocOutBaseDocs_DocId",
                table: "DocOutBaseDocs",
                column: "DocId");

            migrationBuilder.CreateIndex(
                name: "IX_DocOutProducts_DocId",
                table: "DocOutProducts",
                column: "DocId");

            migrationBuilder.CreateIndex(
                name: "IX_DocOutProducts_ProductId",
                table: "DocOutProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_DocOutProductsHistory_ChangeReasonId",
                table: "DocOutProductsHistory",
                column: "ChangeReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_DocOutProductsHistory_DocId",
                table: "DocOutProductsHistory",
                column: "DocId");

            migrationBuilder.CreateIndex(
                name: "IX_DocOutProductsHistory_ProductId",
                table: "DocOutProductsHistory",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_DocOutProductsHistory_UserId",
                table: "DocOutProductsHistory",
                column: "UserId");

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
                name: "IX_DocsInHistory_DocId",
                table: "DocsInHistory",
                column: "DocId");

            migrationBuilder.CreateIndex(
                name: "IX_DocsInHistory_StatusKey",
                table: "DocsInHistory",
                column: "StatusKey");

            migrationBuilder.CreateIndex(
                name: "IX_DocsInHistory_UserId",
                table: "DocsInHistory",
                column: "UserId");

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

            migrationBuilder.CreateIndex(
                name: "IX_DocsOutHistory_DocId",
                table: "DocsOutHistory",
                column: "DocId");

            migrationBuilder.CreateIndex(
                name: "IX_DocsOutHistory_StatusKey",
                table: "DocsOutHistory",
                column: "StatusKey");

            migrationBuilder.CreateIndex(
                name: "IX_DocsOutHistory_UserId",
                table: "DocsOutHistory",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "DocInBaseDocs");

            migrationBuilder.DropTable(
                name: "DocInProducts");

            migrationBuilder.DropTable(
                name: "DocInProductsHistory");

            migrationBuilder.DropTable(
                name: "DocOutBaseDocs");

            migrationBuilder.DropTable(
                name: "DocOutProducts");

            migrationBuilder.DropTable(
                name: "DocOutProductsHistory");

            migrationBuilder.DropTable(
                name: "DocsInHistory");

            migrationBuilder.DropTable(
                name: "DocsOutHistory");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "BaseDocs");

            migrationBuilder.DropTable(
                name: "ChangeReasons");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "DocsIn");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "DocsOut");

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
