using Microsoft.EntityFrameworkCore.Migrations;

namespace Hippra.Migrations
{
    public partial class removeTag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CaseTag");

            migrationBuilder.AddColumn<int>(
                name: "TagID",
                table: "Cases",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cases_TagID",
                table: "Cases",
                column: "TagID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Tags_TagID",
                table: "Cases",
                column: "TagID",
                principalTable: "Tags",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Tags_TagID",
                table: "Cases");

            migrationBuilder.DropIndex(
                name: "IX_Cases_TagID",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "TagID",
                table: "Cases");

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
    }
}
