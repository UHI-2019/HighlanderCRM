using Microsoft.EntityFrameworkCore.Migrations;

namespace Highlander.Data.Migrations
{
    public partial class ExpandArtefactModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "Artefacts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "UserArchiveById",
                table: "Artefacts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserLastEditedById",
                table: "Artefacts",
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

            migrationBuilder.CreateIndex(
                name: "IX_Artefacts_UserArchiveById",
                table: "Artefacts",
                column: "UserArchiveById");

            migrationBuilder.CreateIndex(
                name: "IX_Artefacts_UserLastEditedById",
                table: "Artefacts",
                column: "UserLastEditedById");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artefacts_AspNetUsers_UserArchiveById",
                table: "Artefacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Artefacts_AspNetUsers_UserLastEditedById",
                table: "Artefacts");

            migrationBuilder.DropIndex(
                name: "IX_Artefacts_UserArchiveById",
                table: "Artefacts");

            migrationBuilder.DropIndex(
                name: "IX_Artefacts_UserLastEditedById",
                table: "Artefacts");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "Artefacts");

            migrationBuilder.DropColumn(
                name: "UserArchiveById",
                table: "Artefacts");

            migrationBuilder.DropColumn(
                name: "UserLastEditedById",
                table: "Artefacts");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "21f93cf0-15e5-4361-a11d-cb4ed38f397b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "a439e973-0b82-46fc-acab-6f6ea78080d0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "e8e2562b-f4da-48ea-af95-e1bdb31e2706");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "ad174b2f-bbf8-4ef3-bca7-9d49a82d78af");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 5,
                column: "ConcurrencyStamp",
                value: "b17ee97a-1652-484c-ab0b-c6398c342201");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "d3be1487-8723-4eb4-8d63-fa3bc9848861");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 7,
                column: "ConcurrencyStamp",
                value: "a115a5a6-a9c1-4f8d-b866-dad05c5a50ed");
        }
    }
}
