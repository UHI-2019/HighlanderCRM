using Microsoft.EntityFrameworkCore.Migrations;

namespace Highlander.Data.Migrations
{
    public partial class AddBranchExpandedModelsTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessSectors_CommercialContacts_CommercialContactId",
                table: "BusinessSectors");

            migrationBuilder.DropForeignKey(
                name: "FK_CommercialContacts_UserCommercialContacts_UserCommercialCont~",
                table: "CommercialContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Decorations_AspNetUsers_ApplicationUserId",
                table: "Decorations");

            migrationBuilder.DropForeignKey(
                name: "FK_EmergencyContacts_Staff_StaffId",
                table: "EmergencyContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_EmergencyContacts_Volunteers_VolunteerId",
                table: "EmergencyContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Expertises_Volunteers_VolunteerId",
                table: "Expertises");

            migrationBuilder.DropForeignKey(
                name: "FK_Regiments_Regimentals_RegimentalId",
                table: "Regiments");

            migrationBuilder.DropIndex(
                name: "IX_Regiments_RegimentalId",
                table: "Regiments");

            migrationBuilder.DropIndex(
                name: "IX_Expertises_VolunteerId",
                table: "Expertises");

            migrationBuilder.DropIndex(
                name: "IX_EmergencyContacts_StaffId",
                table: "EmergencyContacts");

            migrationBuilder.DropIndex(
                name: "IX_EmergencyContacts_VolunteerId",
                table: "EmergencyContacts");

            migrationBuilder.DropIndex(
                name: "IX_Decorations_ApplicationUserId",
                table: "Decorations");

            migrationBuilder.DropIndex(
                name: "IX_CommercialContacts_UserCommercialContactId",
                table: "CommercialContacts");

            migrationBuilder.DropIndex(
                name: "IX_BusinessSectors_CommercialContactId",
                table: "BusinessSectors");

            migrationBuilder.DropColumn(
                name: "RegimentalId",
                table: "Regiments");

            migrationBuilder.DropColumn(
                name: "VolunteerId",
                table: "Expertises");

            migrationBuilder.DropColumn(
                name: "StaffId",
                table: "EmergencyContacts");

            migrationBuilder.DropColumn(
                name: "VolunteerId",
                table: "EmergencyContacts");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Decorations");

            migrationBuilder.DropColumn(
                name: "UserCommercialContactId",
                table: "CommercialContacts");

            migrationBuilder.DropColumn(
                name: "CommercialContactId",
                table: "BusinessSectors");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Regiments",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "ArtefactId",
                table: "DonorArtefacts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DecorationId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VolunteerId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Volunteers_EmergencyContactId",
                table: "Volunteers",
                column: "EmergencyContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Volunteers_ExpertiseId",
                table: "Volunteers",
                column: "ExpertiseId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCommercialContacts_CommercialContactId",
                table: "UserCommercialContacts",
                column: "CommercialContactId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCommercialContacts_UserId",
                table: "UserCommercialContacts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_EmergencyContactId",
                table: "Staff",
                column: "EmergencyContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_UserId",
                table: "Staff",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Regimentals_RegimentId",
                table: "Regimentals",
                column: "RegimentId");

            migrationBuilder.CreateIndex(
                name: "IX_Regimentals_UserId",
                table: "Regimentals",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Members_UserId",
                table: "Members",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Donors_UserId",
                table: "Donors",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DonorArtefacts_ArtefactId",
                table: "DonorArtefacts",
                column: "ArtefactId");

            migrationBuilder.CreateIndex(
                name: "IX_DonorArtefacts_DonorId",
                table: "DonorArtefacts",
                column: "DonorId");

            migrationBuilder.CreateIndex(
                name: "IX_CommercialContacts_BusinessSectorId",
                table: "CommercialContacts",
                column: "BusinessSectorId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DecorationId",
                table: "AspNetUsers",
                column: "DecorationId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_VolunteerId",
                table: "AspNetUsers",
                column: "VolunteerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Decorations_DecorationId",
                table: "AspNetUsers",
                column: "DecorationId",
                principalTable: "Decorations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Volunteers_VolunteerId",
                table: "AspNetUsers",
                column: "VolunteerId",
                principalTable: "Volunteers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CommercialContacts_BusinessSectors_BusinessSectorId",
                table: "CommercialContacts",
                column: "BusinessSectorId",
                principalTable: "BusinessSectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DonorArtefacts_Artefacts_ArtefactId",
                table: "DonorArtefacts",
                column: "ArtefactId",
                principalTable: "Artefacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DonorArtefacts_Donors_DonorId",
                table: "DonorArtefacts",
                column: "DonorId",
                principalTable: "Donors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Donors_AspNetUsers_UserId",
                table: "Donors",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Members_AspNetUsers_UserId",
                table: "Members",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Regimentals_Regiments_RegimentId",
                table: "Regimentals",
                column: "RegimentId",
                principalTable: "Regiments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Regimentals_AspNetUsers_UserId",
                table: "Regimentals",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Staff_EmergencyContacts_EmergencyContactId",
                table: "Staff",
                column: "EmergencyContactId",
                principalTable: "EmergencyContacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Staff_AspNetUsers_UserId",
                table: "Staff",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCommercialContacts_CommercialContacts_CommercialContactId",
                table: "UserCommercialContacts",
                column: "CommercialContactId",
                principalTable: "CommercialContacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCommercialContacts_AspNetUsers_UserId",
                table: "UserCommercialContacts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Volunteers_EmergencyContacts_EmergencyContactId",
                table: "Volunteers",
                column: "EmergencyContactId",
                principalTable: "EmergencyContacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Volunteers_Expertises_ExpertiseId",
                table: "Volunteers",
                column: "ExpertiseId",
                principalTable: "Expertises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Decorations_DecorationId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Volunteers_VolunteerId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_CommercialContacts_BusinessSectors_BusinessSectorId",
                table: "CommercialContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_DonorArtefacts_Artefacts_ArtefactId",
                table: "DonorArtefacts");

            migrationBuilder.DropForeignKey(
                name: "FK_DonorArtefacts_Donors_DonorId",
                table: "DonorArtefacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Donors_AspNetUsers_UserId",
                table: "Donors");

            migrationBuilder.DropForeignKey(
                name: "FK_Members_AspNetUsers_UserId",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_Regimentals_Regiments_RegimentId",
                table: "Regimentals");

            migrationBuilder.DropForeignKey(
                name: "FK_Regimentals_AspNetUsers_UserId",
                table: "Regimentals");

            migrationBuilder.DropForeignKey(
                name: "FK_Staff_EmergencyContacts_EmergencyContactId",
                table: "Staff");

            migrationBuilder.DropForeignKey(
                name: "FK_Staff_AspNetUsers_UserId",
                table: "Staff");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCommercialContacts_CommercialContacts_CommercialContactId",
                table: "UserCommercialContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCommercialContacts_AspNetUsers_UserId",
                table: "UserCommercialContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Volunteers_EmergencyContacts_EmergencyContactId",
                table: "Volunteers");

            migrationBuilder.DropForeignKey(
                name: "FK_Volunteers_Expertises_ExpertiseId",
                table: "Volunteers");

            migrationBuilder.DropIndex(
                name: "IX_Volunteers_EmergencyContactId",
                table: "Volunteers");

            migrationBuilder.DropIndex(
                name: "IX_Volunteers_ExpertiseId",
                table: "Volunteers");

            migrationBuilder.DropIndex(
                name: "IX_UserCommercialContacts_CommercialContactId",
                table: "UserCommercialContacts");

            migrationBuilder.DropIndex(
                name: "IX_UserCommercialContacts_UserId",
                table: "UserCommercialContacts");

            migrationBuilder.DropIndex(
                name: "IX_Staff_EmergencyContactId",
                table: "Staff");

            migrationBuilder.DropIndex(
                name: "IX_Staff_UserId",
                table: "Staff");

            migrationBuilder.DropIndex(
                name: "IX_Regimentals_RegimentId",
                table: "Regimentals");

            migrationBuilder.DropIndex(
                name: "IX_Regimentals_UserId",
                table: "Regimentals");

            migrationBuilder.DropIndex(
                name: "IX_Members_UserId",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Donors_UserId",
                table: "Donors");

            migrationBuilder.DropIndex(
                name: "IX_DonorArtefacts_ArtefactId",
                table: "DonorArtefacts");

            migrationBuilder.DropIndex(
                name: "IX_DonorArtefacts_DonorId",
                table: "DonorArtefacts");

            migrationBuilder.DropIndex(
                name: "IX_CommercialContacts_BusinessSectorId",
                table: "CommercialContacts");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DecorationId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_VolunteerId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ArtefactId",
                table: "DonorArtefacts");

            migrationBuilder.DropColumn(
                name: "DecorationId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "VolunteerId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Regiments",
                newName: "name");

            migrationBuilder.AddColumn<int>(
                name: "RegimentalId",
                table: "Regiments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VolunteerId",
                table: "Expertises",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StaffId",
                table: "EmergencyContacts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VolunteerId",
                table: "EmergencyContacts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApplicationUserId",
                table: "Decorations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserCommercialContactId",
                table: "CommercialContacts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CommercialContactId",
                table: "BusinessSectors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Regiments_RegimentalId",
                table: "Regiments",
                column: "RegimentalId");

            migrationBuilder.CreateIndex(
                name: "IX_Expertises_VolunteerId",
                table: "Expertises",
                column: "VolunteerId");

            migrationBuilder.CreateIndex(
                name: "IX_EmergencyContacts_StaffId",
                table: "EmergencyContacts",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_EmergencyContacts_VolunteerId",
                table: "EmergencyContacts",
                column: "VolunteerId");

            migrationBuilder.CreateIndex(
                name: "IX_Decorations_ApplicationUserId",
                table: "Decorations",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CommercialContacts_UserCommercialContactId",
                table: "CommercialContacts",
                column: "UserCommercialContactId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessSectors_CommercialContactId",
                table: "BusinessSectors",
                column: "CommercialContactId");

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
                name: "FK_Decorations_AspNetUsers_ApplicationUserId",
                table: "Decorations",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Regiments_Regimentals_RegimentalId",
                table: "Regiments",
                column: "RegimentalId",
                principalTable: "Regimentals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
