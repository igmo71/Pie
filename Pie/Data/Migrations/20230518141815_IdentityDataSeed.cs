using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pie.Data.Migrations
{
    /// <inheritdoc />
    public partial class IdentityDataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0b60c927-0e9f-4fa7-8422-e0e16e6fa5f4");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "WarehouseId" },
                values: new object[] { "d90e31c9-e19f-4ee7-9580-d856daba6d02", 0, "c9023eae-8542-460f-af6c-fb2361ae2be0", "Service1c@www", true, "Service1c", null, true, null, "SERVICE1C@WWW", "SERVICE1C@WWW", "AQAAAAIAAYagAAAAEAP/xtaltm7cuB/Bk/sRF/GDtCtQf9B1ghEEbr6eprNlsKYsaGt5ncmcR/utO76tWw==", null, false, "6WMMOSBLWGF45HZLH5OJIQADMFB6YJGQ", false, "Service1c@www", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d90e31c9-e19f-4ee7-9580-d856daba6d02");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "WarehouseId" },
                values: new object[] { "0b60c927-0e9f-4fa7-8422-e0e16e6fa5f4", 0, "655eae0d-3ab4-4688-87ec-700d6c6c9567", "Service1c@www", true, "Service1c", null, true, null, "SERVICE1C@WWW", "SERVICE1C@WWW", "AQAAAAIAAYagAAAAEAOaCAADpFz/XmkyKhEkd0FjnlPHtkUSiV7GdH10SvSKvK4eZhtJHFnWl66jxbydXw==", null, false, "JCSGTYDEWVEPHPV7DDVTSY263NY4JDBP", false, "Service1c@www", null });
        }
    }
}
