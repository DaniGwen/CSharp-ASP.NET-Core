using Microsoft.EntityFrameworkCore.Migrations;

namespace KniveGallery.Web.Migrations
{
    public partial class orderMod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KniveId",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Knives",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "KniveId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
