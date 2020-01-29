using Microsoft.EntityFrameworkCore.Migrations;

namespace DigitalCoolBook.App.Migrations
{
    public partial class _006 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_StudentId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_StudentId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StudentId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_StudentId",
                table: "AspNetUsers",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_StudentId",
                table: "AspNetUsers",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
