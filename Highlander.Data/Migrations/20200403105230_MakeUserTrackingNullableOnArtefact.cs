using Microsoft.EntityFrameworkCore.Migrations;

namespace Highlander.Data.Migrations
{
    public partial class MakeUserTrackingNullableOnArtefact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artefacts_AspNetUsers_UserArchiveById",
                table: "Artefacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Artefacts_AspNetUsers_UserLastEditedById",
                table: "Artefacts");

            migrationBuilder.AlterColumn<int>(
                name: "UserLastEditedById",
                table: "Artefacts",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "UserArchiveById",
                table: "Artefacts",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "fc6b4bb9-44eb-4f09-b287-9e79a53c8159");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "279b37f9-b7a3-4ee4-abc0-157b2bb5e208");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "31e59913-4e31-4697-b539-6e1d5becc592");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "77a0cf84-4c15-4d66-9198-14b4a3a34e63");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 5,
                column: "ConcurrencyStamp",
                value: "eea8447c-ab47-4837-8b10-95e733180603");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "1ed6b238-75f7-4845-80fa-2d6cd4a9c909");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 7,
                column: "ConcurrencyStamp",
                value: "7dc1435d-1299-4a2a-9023-e8a6438c50fe");

            migrationBuilder.AddForeignKey(
                name: "FK_Artefacts_AspNetUsers_UserArchiveById",
                table: "Artefacts",
                column: "UserArchiveById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Artefacts_AspNetUsers_UserLastEditedById",
                table: "Artefacts",
                column: "UserLastEditedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artefacts_AspNetUsers_UserArchiveById",
                table: "Artefacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Artefacts_AspNetUsers_UserLastEditedById",
                table: "Artefacts");

            migrationBuilder.AlterColumn<int>(
                name: "UserLastEditedById",
                table: "Artefacts",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserArchiveById",
                table: "Artefacts",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "6bf820e8-e3ea-4b4a-b339-cccf0a22a568");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "9a8c8c03-0e83-40aa-ad20-11c6b36aa36a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "7ad421db-8909-4755-90e3-afae9584c8db");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "89e40306-7033-424c-9d09-1aa3f4074b19");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 5,
                column: "ConcurrencyStamp",
                value: "2564f16f-d076-4804-9764-f405c4eca3a8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "c388bd4a-189e-41a9-adb2-cc70258329b3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 7,
                column: "ConcurrencyStamp",
                value: "4932447f-aafb-41e7-98f7-0b9ec25c20d2");

            migrationBuilder.AddForeignKey(
                name: "FK_Artefacts_AspNetUsers_UserArchiveById",
                table: "Artefacts",
                column: "UserArchiveById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Artefacts_AspNetUsers_UserLastEditedById",
                table: "Artefacts",
                column: "UserLastEditedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
