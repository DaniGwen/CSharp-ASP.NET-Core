using Microsoft.EntityFrameworkCore.Migrations;

namespace DigitalCoolBook.Data.Migrations
{
    public partial class _017 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Lessons_LessonId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_LessonId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "LessonId",
                table: "Subjects");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LessonId",
                table: "Subjects",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_LessonId",
                table: "Subjects",
                column: "LessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Lessons_LessonId",
                table: "Subjects",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
