using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Highlander.Data.Migrations
{
    public partial class AddTableSiteOptionsAndSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SiteOptions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteOptions", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "SiteOptions",
                columns: new[] { "Id", "Name", "Value" },
                values: new object[] { 1, "site-name", "Highlander Museum" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SiteOptions");

            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "DonorArtefacts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
