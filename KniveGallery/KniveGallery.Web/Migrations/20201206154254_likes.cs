using Microsoft.EntityFrameworkCore.Migrations;

namespace KniveGallery.Web.Migrations
{
    public partial class likes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "KniveId",
                table: "Knives",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "Knives",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Likes",
                table: "Knives");

            migrationBuilder.AlterColumn<int>(
                name: "KniveId",
                table: "Knives",
                type: "int",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("SqlServer:Identity", "1, 1");
        }
    }
}
