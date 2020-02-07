using Microsoft.EntityFrameworkCore.Migrations;

namespace DigitalCoolBook.Data.Migrations
{
    public partial class _014 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Cathegories_CathegoryId",
                table: "Lessons");

            migrationBuilder.DropTable(
                name: "Cathegories");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_CathegoryId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "CathegoryId",
                table: "Lessons");

            migrationBuilder.AddColumn<string>(
                name: "CategoryId",
                table: "Lessons",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    SubjectId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "SubjectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_CategoryId",
                table: "Lessons",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_SubjectId",
                table: "Categories",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Categories_CategoryId",
                table: "Lessons",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Categories_CategoryId",
                table: "Lessons");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_CategoryId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Lessons");

            migrationBuilder.AddColumn<string>(
                name: "CathegoryId",
                table: "Lessons",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Cathegories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SubjectId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cathegories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cathegories_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "SubjectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_CathegoryId",
                table: "Lessons",
                column: "CathegoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Cathegories_SubjectId",
                table: "Cathegories",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Cathegories_CathegoryId",
                table: "Lessons",
                column: "CathegoryId",
                principalTable: "Cathegories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
