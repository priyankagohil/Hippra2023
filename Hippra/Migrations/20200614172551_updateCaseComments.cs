using Microsoft.EntityFrameworkCore.Migrations;

namespace Hippra.Migrations
{
    public partial class updateCaseComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "imgUrl",
                table: "CaseComments",
                newName: "ImgUrl");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImgUrl",
                table: "CaseComments",
                newName: "imgUrl");
        }
    }
}
