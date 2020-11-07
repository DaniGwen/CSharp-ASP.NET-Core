using Microsoft.EntityFrameworkCore.Migrations;

namespace KniveGallery.Web.Data.Migrations
{
    public partial class init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BladeMade",
                table: "Knives");

            migrationBuilder.DropColumn(
                name: "HandleType",
                table: "Knives");

            migrationBuilder.DropColumn(
                name: "KniveName",
                table: "Knives");

            migrationBuilder.DropColumn(
                name: "Length",
                table: "Knives");

            migrationBuilder.AddColumn<string>(
                name: "EdgeMade",
                table: "Knives",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "EdgeTickness",
                table: "Knives",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "EdgeWidth",
                table: "Knives",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HandleDescription",
                table: "Knives",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "TotalLength",
                table: "Knives",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EdgeMade",
                table: "Knives");

            migrationBuilder.DropColumn(
                name: "EdgeTickness",
                table: "Knives");

            migrationBuilder.DropColumn(
                name: "EdgeWidth",
                table: "Knives");

            migrationBuilder.DropColumn(
                name: "HandleDescription",
                table: "Knives");

            migrationBuilder.DropColumn(
                name: "TotalLength",
                table: "Knives");

            migrationBuilder.AddColumn<string>(
                name: "BladeMade",
                table: "Knives",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HandleType",
                table: "Knives",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "KniveName",
                table: "Knives",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Length",
                table: "Knives",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
