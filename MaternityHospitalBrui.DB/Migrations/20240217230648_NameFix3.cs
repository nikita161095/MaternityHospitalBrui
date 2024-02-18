using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaternityHospitalBrui.DB.Migrations
{
    public partial class NameFix3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NameId",
                table: "Names",
                newName: "Name2Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name2Id",
                table: "Names",
                newName: "NameId");
        }
    }
}
