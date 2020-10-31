using Microsoft.EntityFrameworkCore.Migrations;

namespace KniveGallery.Web.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Knives",
                columns: table => new
                {
                    KniveId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KniveName = table.Column<string>(nullable: false),
                    Length = table.Column<double>(nullable: false),
                    EdgeLength = table.Column<double>(nullable: false),
                    HandleType = table.Column<string>(nullable: false),
                    BladeMade = table.Column<string>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    ImagePath = table.Column<string>(nullable: true),
                    KniveClass = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Knives", x => x.KniveId);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ImageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagePath = table.Column<string>(nullable: true),
                    KniveId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_Images_Knives_KniveId",
                        column: x => x.KniveId,
                        principalTable: "Knives",
                        principalColumn: "KniveId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_KniveId",
                table: "Images",
                column: "KniveId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Knives");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "AspNetUsers");
        }
    }
}
