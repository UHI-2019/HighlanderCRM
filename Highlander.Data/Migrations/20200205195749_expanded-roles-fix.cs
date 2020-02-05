using Microsoft.EntityFrameworkCore.Migrations;

namespace Highlander.Data.Migrations
{
    public partial class expandedrolesfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Volunteers",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "telNo",
                table: "EmergencyContact",
                newName: "TelNo");

            migrationBuilder.RenameColumn(
                name: "relation",
                table: "EmergencyContact",
                newName: "Relation");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "EmergencyContact",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "EmergencyContactId",
                table: "Volunteers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExpertiseId",
                table: "Volunteers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CommercialContactId",
                table: "UserCommercialContacts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmergencyContactId",
                table: "Staff",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BusinessSectorId",
                table: "CommercialContact",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmergencyContactId",
                table: "Volunteers");

            migrationBuilder.DropColumn(
                name: "ExpertiseId",
                table: "Volunteers");

            migrationBuilder.DropColumn(
                name: "CommercialContactId",
                table: "UserCommercialContacts");

            migrationBuilder.DropColumn(
                name: "EmergencyContactId",
                table: "Staff");

            migrationBuilder.DropColumn(
                name: "BusinessSectorId",
                table: "CommercialContact");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Volunteers",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "TelNo",
                table: "EmergencyContact",
                newName: "telNo");

            migrationBuilder.RenameColumn(
                name: "Relation",
                table: "EmergencyContact",
                newName: "relation");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "EmergencyContact",
                newName: "name");
        }
    }
}
