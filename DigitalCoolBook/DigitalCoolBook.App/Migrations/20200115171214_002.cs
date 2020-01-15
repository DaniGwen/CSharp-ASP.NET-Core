using Microsoft.EntityFrameworkCore.Migrations;

namespace DigitalCoolBook.App.Migrations
{
    public partial class _002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Teachers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Students",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Students");
        }
    }
}
