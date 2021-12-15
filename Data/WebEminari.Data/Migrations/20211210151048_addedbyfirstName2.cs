using Microsoft.EntityFrameworkCore.Migrations;

namespace WebEminari.Data.Migrations
{
    public partial class addedbyfirstName2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AddByUserFirstName",
                table: "WebEminars",
                newName: "AddedByUserFirstName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AddedByUserFirstName",
                table: "WebEminars",
                newName: "AddByUserFirstName");
        }
    }
}
