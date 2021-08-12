using Microsoft.EntityFrameworkCore.Migrations;

namespace DigitalCoolBook.Data.Migrations
{
    public partial class liveMessagesUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LiveFeedMessages_AspNetUsers_TeacherId",
                table: "LiveFeedMessages");

            migrationBuilder.DropIndex(
                name: "IX_LiveFeedMessages_TeacherId",
                table: "LiveFeedMessages");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "LiveFeedMessages");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "LiveFeedMessages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "LiveFeedMessages",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "LiveFeedMessages");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "LiveFeedMessages");

            migrationBuilder.AddColumn<string>(
                name: "TeacherId",
                table: "LiveFeedMessages",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LiveFeedMessages_TeacherId",
                table: "LiveFeedMessages",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_LiveFeedMessages_AspNetUsers_TeacherId",
                table: "LiveFeedMessages",
                column: "TeacherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
