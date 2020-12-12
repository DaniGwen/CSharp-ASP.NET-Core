using Microsoft.EntityFrameworkCore.Migrations;

namespace KniveGallery.Web.Migrations
{
    public partial class carousel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarouselImages",
                columns: table => new
                {
                    ImageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarouselImages", x => x.ImageId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarouselImages");
        }
    }
}
