using Microsoft.EntityFrameworkCore.Migrations;

namespace Highlander.Data.Migrations
{
    public partial class InsertApplicationRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "b8671fec-e9d5-477c-b5e5-a56c3542f977", "Superuser", null },
                    { 2, "b0621cff-283e-4b3b-9ec2-65116d4e26a6", "Administrator", null },
                    { 3, "5ac93523-d1f1-475d-bb43-f730230ed7e6", "Staff", null },
                    { 4, "dad62ed2-6bf0-4ca1-9c61-ad543e1c82f6", "Volunteer", null },
                    { 5, "64342c9a-3d08-4608-a55d-6f3055e16170", "Member", null },
                    { 6, "2a5412c8-6cec-4ebe-a013-75621c9ef6b4", "Donor", null },
                    { 7, "f2a98203-a7f3-450f-95ab-33bede6d788b", "Regimental", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 7);
        }
    }
}
