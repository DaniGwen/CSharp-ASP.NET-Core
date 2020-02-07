using Microsoft.EntityFrameworkCore.Migrations;

namespace DigitalCoolBook.Data.Migrations
{
    public partial class _018 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Lessons_LessonId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Categories_CategoryId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_CategoryId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Categories_LessonId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "LessonId",
                table: "Categories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CategoryId",
                table: "Subjects",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LessonId",
                table: "Categories",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_CategoryId",
                table: "Subjects",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_LessonId",
                table: "Categories",
                column: "LessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Lessons_LessonId",
                table: "Categories",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Categories_CategoryId",
                table: "Subjects",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
