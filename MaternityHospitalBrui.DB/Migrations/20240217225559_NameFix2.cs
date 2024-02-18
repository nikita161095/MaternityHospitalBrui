using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaternityHospitalBrui.DB.Migrations
{
    public partial class NameFix2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Name_NameId",
                table: "Patients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Name",
                table: "Name");

            migrationBuilder.RenameTable(
                name: "Name",
                newName: "Names");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Names",
                table: "Names",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Names_NameId",
                table: "Patients",
                column: "NameId",
                principalTable: "Names",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Names_NameId",
                table: "Patients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Names",
                table: "Names");

            migrationBuilder.RenameTable(
                name: "Names",
                newName: "Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Name",
                table: "Name",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Name_NameId",
                table: "Patients",
                column: "NameId",
                principalTable: "Name",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
