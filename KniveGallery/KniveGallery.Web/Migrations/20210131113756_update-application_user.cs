using Microsoft.EntityFrameworkCore.Migrations;

namespace KniveGallery.Web.Migrations
{
    public partial class updateapplication_user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Facebook",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Facebook",
                table: "AspNetUsers");
        }
    }
}
