using Microsoft.EntityFrameworkCore.Migrations;

namespace Hippra.Migrations
{
    public partial class addCaseCommentImgField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "imgUrl",
                table: "CaseComments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "imgUrl",
                table: "CaseComments");
        }
    }
}
