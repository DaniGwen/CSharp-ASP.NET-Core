using Microsoft.EntityFrameworkCore.Migrations;

namespace DigitalCoolBook.Data.Migrations
{
    public partial class _02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Categories_CategoryId",
                table: "Lessons");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Categories_CategoryId",
                table: "Lessons",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Categories_CategoryId",
                table: "Lessons");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Categories_CategoryId",
                table: "Lessons",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
