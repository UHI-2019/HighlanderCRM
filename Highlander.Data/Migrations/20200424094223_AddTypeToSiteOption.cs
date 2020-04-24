using Microsoft.EntityFrameworkCore.Migrations;

namespace Highlander.Data.Migrations
{
    public partial class AddTypeToSiteOption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "SiteOptions",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "472e90cf-34b3-40be-8172-1a360710ab47");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "3d5fe3b0-ec2a-466e-bbf0-b16cf20d998a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "ac212415-03a2-4155-9bc1-d36d62286fe4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "3b9b72a6-ff23-40cb-b88a-ae28602b3595");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 5,
                column: "ConcurrencyStamp",
                value: "c91c1655-1a5a-4704-ae98-c0836b0d47e6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "b890a1a4-b483-4bcc-9c67-8a971892840d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 7,
                column: "ConcurrencyStamp",
                value: "ea8a2fc5-40bf-4820-a869-1b0a30c69a67");

            migrationBuilder.UpdateData(
                table: "SiteOptions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Type", "Value" },
                values: new object[] { "text", "Highlander" });

            migrationBuilder.InsertData(
                table: "SiteOptions",
                columns: new[] { "Id", "Name", "Type", "Value" },
                values: new object[] { 2, "primary-colour", "color", "#007bff" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SiteOptions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "Type",
                table: "SiteOptions");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "78ec9fea-1371-4d94-940c-132846bc0126");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "03923f9c-01c6-4665-9758-8650631db94e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "9fc333bc-dad2-4184-8928-461195bc90f7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "b9ae7ac9-f2e7-41b9-80a4-a15d5dbabd78");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 5,
                column: "ConcurrencyStamp",
                value: "6bb04ebd-8f48-4b86-ba26-177003292ec1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "2cbbb102-39a5-4eb8-947d-45479a23293d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 7,
                column: "ConcurrencyStamp",
                value: "786388e5-088d-4038-a156-51e95f19c24f");

            migrationBuilder.UpdateData(
                table: "SiteOptions",
                keyColumn: "Id",
                keyValue: 1,
                column: "Value",
                value: "Highlander Museum");
        }
    }
}
