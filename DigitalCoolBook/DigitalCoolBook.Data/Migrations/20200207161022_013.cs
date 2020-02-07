using Microsoft.EntityFrameworkCore.Migrations;

namespace DigitalCoolBook.Data.Migrations
{
    public partial class _013 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Subjects_SubjectId",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_SubjectId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "Lessons");

            migrationBuilder.AddColumn<string>(
                name: "CathegoryId",
                table: "Lessons",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Cathegories",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    SubjectId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cathegories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cathegories_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "SubjectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_CathegoryId",
                table: "Lessons",
                column: "CathegoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Cathegories_SubjectId",
                table: "Cathegories",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Cathegories_CathegoryId",
                table: "Lessons",
                column: "CathegoryId",
                principalTable: "Cathegories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Cathegories_CathegoryId",
                table: "Lessons");

            migrationBuilder.DropTable(
                name: "Cathegories");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_CathegoryId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "CathegoryId",
                table: "Lessons");

            migrationBuilder.AddColumn<string>(
                name: "SubjectId",
                table: "Lessons",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_SubjectId",
                table: "Lessons",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Subjects_SubjectId",
                table: "Lessons",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "SubjectId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
