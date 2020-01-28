using Microsoft.EntityFrameworkCore.Migrations;

namespace DigitalCoolBook.App.Migrations
{
    public partial class _004 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_GradeParalelos_IdGradeParalelo",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_IdGradeParalelo",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IdGradeParalelo",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "GradeId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GradeParaleloId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_GradeId",
                table: "AspNetUsers",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_GradeParaleloId",
                table: "AspNetUsers",
                column: "GradeParaleloId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Grades_GradeId",
                table: "AspNetUsers",
                column: "GradeId",
                principalTable: "Grades",
                principalColumn: "GradeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_GradeParalelos_GradeParaleloId",
                table: "AspNetUsers",
                column: "GradeParaleloId",
                principalTable: "GradeParalelos",
                principalColumn: "GradeParaleloId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Grades_GradeId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_GradeParalelos_GradeParaleloId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_GradeId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_GradeParaleloId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GradeId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GradeParaleloId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "IdGradeParalelo",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_IdGradeParalelo",
                table: "AspNetUsers",
                column: "IdGradeParalelo");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_GradeParalelos_IdGradeParalelo",
                table: "AspNetUsers",
                column: "IdGradeParalelo",
                principalTable: "GradeParalelos",
                principalColumn: "GradeParaleloId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
