using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Highlander.Data.Migrations
{
    public partial class expandedrolesfix2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessSector_CommercialContact_CommercialContactId",
                table: "BusinessSector");

            migrationBuilder.DropForeignKey(
                name: "FK_CommercialContact_UserCommercialContacts_UserCommercialConta~",
                table: "CommercialContact");

            migrationBuilder.DropForeignKey(
                name: "FK_EmergencyContact_Staff_StaffId",
                table: "EmergencyContact");

            migrationBuilder.DropForeignKey(
                name: "FK_EmergencyContact_Volunteers_VolunteerId",
                table: "EmergencyContact");

            migrationBuilder.DropForeignKey(
                name: "FK_Expertise_Volunteers_VolunteerId",
                table: "Expertise");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Expertise",
                table: "Expertise");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmergencyContact",
                table: "EmergencyContact");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommercialContact",
                table: "CommercialContact");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BusinessSector",
                table: "BusinessSector");

            migrationBuilder.RenameTable(
                name: "Expertise",
                newName: "Expertises");

            migrationBuilder.RenameTable(
                name: "EmergencyContact",
                newName: "EmergencyContacts");

            migrationBuilder.RenameTable(
                name: "CommercialContact",
                newName: "CommercialContacts");

            migrationBuilder.RenameTable(
                name: "BusinessSector",
                newName: "BusinessSectors");

            migrationBuilder.RenameIndex(
                name: "IX_Expertise_VolunteerId",
                table: "Expertises",
                newName: "IX_Expertises_VolunteerId");

            migrationBuilder.RenameIndex(
                name: "IX_EmergencyContact_VolunteerId",
                table: "EmergencyContacts",
                newName: "IX_EmergencyContacts_VolunteerId");

            migrationBuilder.RenameIndex(
                name: "IX_EmergencyContact_StaffId",
                table: "EmergencyContacts",
                newName: "IX_EmergencyContacts_StaffId");

            migrationBuilder.RenameIndex(
                name: "IX_CommercialContact_UserCommercialContactId",
                table: "CommercialContacts",
                newName: "IX_CommercialContacts_UserCommercialContactId");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessSector_CommercialContactId",
                table: "BusinessSectors",
                newName: "IX_BusinessSectors_CommercialContactId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Expertises",
                table: "Expertises",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmergencyContacts",
                table: "EmergencyContacts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommercialContacts",
                table: "CommercialContacts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BusinessSectors",
                table: "BusinessSectors",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Number = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    ExpiryDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regimentals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    RegimentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regimentals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MemberArchives",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MemberId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Number = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    ExpiryDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberArchives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemberArchives_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Regiments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: true),
                    RegimentalId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regiments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Regiments_Regimentals_RegimentalId",
                        column: x => x.RegimentalId,
                        principalTable: "Regimentals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MemberArchives_MemberId",
                table: "MemberArchives",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Regiments_RegimentalId",
                table: "Regiments",
                column: "RegimentalId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessSectors_CommercialContacts_CommercialContactId",
                table: "BusinessSectors",
                column: "CommercialContactId",
                principalTable: "CommercialContacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CommercialContacts_UserCommercialContacts_UserCommercialCont~",
                table: "CommercialContacts",
                column: "UserCommercialContactId",
                principalTable: "UserCommercialContacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmergencyContacts_Staff_StaffId",
                table: "EmergencyContacts",
                column: "StaffId",
                principalTable: "Staff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmergencyContacts_Volunteers_VolunteerId",
                table: "EmergencyContacts",
                column: "VolunteerId",
                principalTable: "Volunteers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Expertises_Volunteers_VolunteerId",
                table: "Expertises",
                column: "VolunteerId",
                principalTable: "Volunteers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessSectors_CommercialContacts_CommercialContactId",
                table: "BusinessSectors");

            migrationBuilder.DropForeignKey(
                name: "FK_CommercialContacts_UserCommercialContacts_UserCommercialCont~",
                table: "CommercialContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_EmergencyContacts_Staff_StaffId",
                table: "EmergencyContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_EmergencyContacts_Volunteers_VolunteerId",
                table: "EmergencyContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Expertises_Volunteers_VolunteerId",
                table: "Expertises");

            migrationBuilder.DropTable(
                name: "MemberArchives");

            migrationBuilder.DropTable(
                name: "Regiments");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Regimentals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Expertises",
                table: "Expertises");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmergencyContacts",
                table: "EmergencyContacts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommercialContacts",
                table: "CommercialContacts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BusinessSectors",
                table: "BusinessSectors");

            migrationBuilder.RenameTable(
                name: "Expertises",
                newName: "Expertise");

            migrationBuilder.RenameTable(
                name: "EmergencyContacts",
                newName: "EmergencyContact");

            migrationBuilder.RenameTable(
                name: "CommercialContacts",
                newName: "CommercialContact");

            migrationBuilder.RenameTable(
                name: "BusinessSectors",
                newName: "BusinessSector");

            migrationBuilder.RenameIndex(
                name: "IX_Expertises_VolunteerId",
                table: "Expertise",
                newName: "IX_Expertise_VolunteerId");

            migrationBuilder.RenameIndex(
                name: "IX_EmergencyContacts_VolunteerId",
                table: "EmergencyContact",
                newName: "IX_EmergencyContact_VolunteerId");

            migrationBuilder.RenameIndex(
                name: "IX_EmergencyContacts_StaffId",
                table: "EmergencyContact",
                newName: "IX_EmergencyContact_StaffId");

            migrationBuilder.RenameIndex(
                name: "IX_CommercialContacts_UserCommercialContactId",
                table: "CommercialContact",
                newName: "IX_CommercialContact_UserCommercialContactId");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessSectors_CommercialContactId",
                table: "BusinessSector",
                newName: "IX_BusinessSector_CommercialContactId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Expertise",
                table: "Expertise",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmergencyContact",
                table: "EmergencyContact",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommercialContact",
                table: "CommercialContact",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BusinessSector",
                table: "BusinessSector",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessSector_CommercialContact_CommercialContactId",
                table: "BusinessSector",
                column: "CommercialContactId",
                principalTable: "CommercialContact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CommercialContact_UserCommercialContacts_UserCommercialConta~",
                table: "CommercialContact",
                column: "UserCommercialContactId",
                principalTable: "UserCommercialContacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmergencyContact_Staff_StaffId",
                table: "EmergencyContact",
                column: "StaffId",
                principalTable: "Staff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmergencyContact_Volunteers_VolunteerId",
                table: "EmergencyContact",
                column: "VolunteerId",
                principalTable: "Volunteers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Expertise_Volunteers_VolunteerId",
                table: "Expertise",
                column: "VolunteerId",
                principalTable: "Volunteers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
