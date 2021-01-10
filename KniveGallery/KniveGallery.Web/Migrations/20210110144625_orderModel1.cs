using Microsoft.EntityFrameworkCore.Migrations;

namespace KniveGallery.Web.Migrations
{
    public partial class orderModel1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Knives_Orders_OrderId",
                table: "Knives");

            migrationBuilder.DropIndex(
                name: "IX_Knives_OrderId",
                table: "Knives");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Knives");

            migrationBuilder.CreateTable(
                name: "OrderedKniveIds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KniveId = table.Column<int>(nullable: false),
                    OrderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderedKniveIds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderedKniveIds_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderedKniveIds_OrderId",
                table: "OrderedKniveIds",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderedKniveIds");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Knives",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Knives_OrderId",
                table: "Knives",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Knives_Orders_OrderId",
                table: "Knives",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
