using Microsoft.EntityFrameworkCore.Migrations;

namespace Highlander.Data.Migrations
{
    public partial class RenameItemIdToArtefactId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonorArtefacts_Artefacts_ArtefactId",
                table: "DonorArtefacts");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "DonorArtefacts");

            migrationBuilder.AlterColumn<int>(
                name: "ArtefactId",
                table: "DonorArtefacts",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "8848743d-88f2-4459-ac04-49f294b9d762");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "4c49f826-ee06-48fc-949f-07636fd3194e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "4c723a90-4e5d-4d71-a5c4-d520aea70155");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "a0da0029-5cf0-4e1d-bbf4-14367aac94c2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 5,
                column: "ConcurrencyStamp",
                value: "adc8ed00-7425-475c-8214-fb35b117c8b7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "3242227f-4a82-4f05-a6fd-04d2667b9629");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 7,
                column: "ConcurrencyStamp",
                value: "1e006c0a-5658-40a3-b68a-c179d7f8077d");

            migrationBuilder.AddForeignKey(
                name: "FK_DonorArtefacts_Artefacts_ArtefactId",
                table: "DonorArtefacts",
                column: "ArtefactId",
                principalTable: "Artefacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonorArtefacts_Artefacts_ArtefactId",
                table: "DonorArtefacts");

            migrationBuilder.AlterColumn<int>(
                name: "ArtefactId",
                table: "DonorArtefacts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "DonorArtefacts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "7e62e789-2f67-448b-98a7-fe0b3394b239");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "95d2864e-d771-4bdc-ba94-b8035701ea09");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "cd3ae181-1bbc-4701-93c4-02aca4cb8229");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "2a7d3961-743b-49f2-9ca2-469c58573ea3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 5,
                column: "ConcurrencyStamp",
                value: "dcf606c6-46eb-4005-a9f1-3f47c28d1d68");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "b126cb0a-5ee8-47c1-acac-b555eac8e0d9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 7,
                column: "ConcurrencyStamp",
                value: "d3b540cf-6e22-4900-90cd-283724a50548");

            migrationBuilder.AddForeignKey(
                name: "FK_DonorArtefacts_Artefacts_ArtefactId",
                table: "DonorArtefacts",
                column: "ArtefactId",
                principalTable: "Artefacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
