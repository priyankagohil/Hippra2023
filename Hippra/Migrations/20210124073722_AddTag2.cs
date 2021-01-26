using Microsoft.EntityFrameworkCore.Migrations;

namespace Hippra.Migrations
{
    public partial class AddTag2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Cases_CaseID",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_CaseID",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "CaseID",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "TagID",
                table: "Tags");

            migrationBuilder.CreateTable(
                name: "CaseTag",
                columns: table => new
                {
                    CasesID = table.Column<int>(type: "INTEGER", nullable: false),
                    TagsID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseTag", x => new { x.CasesID, x.TagsID });
                    table.ForeignKey(
                        name: "FK_CaseTag_Cases_CasesID",
                        column: x => x.CasesID,
                        principalTable: "Cases",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CaseTag_Tags_TagsID",
                        column: x => x.TagsID,
                        principalTable: "Tags",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CaseTag_TagsID",
                table: "CaseTag",
                column: "TagsID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CaseTag");

            migrationBuilder.AddColumn<int>(
                name: "CaseID",
                table: "Tags",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TagID",
                table: "Tags",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_CaseID",
                table: "Tags",
                column: "CaseID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Cases_CaseID",
                table: "Tags",
                column: "CaseID",
                principalTable: "Cases",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
