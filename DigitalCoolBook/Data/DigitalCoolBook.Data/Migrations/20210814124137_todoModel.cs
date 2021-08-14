using Microsoft.EntityFrameworkCore.Migrations;

namespace DigitalCoolBook.Data.Migrations
{
    public partial class todoModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TodoTasks",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoTasks", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_TodoTasks_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Todos",
                columns: table => new
                {
                    TodoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDone = table.Column<bool>(type: "bit", nullable: false),
                    TodoTaskId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Todos", x => x.TodoId);
                    table.ForeignKey(
                        name: "FK_Todos_TodoTasks_TodoTaskId",
                        column: x => x.TodoTaskId,
                        principalTable: "TodoTasks",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Todos_TodoTaskId",
                table: "Todos",
                column: "TodoTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TodoTasks_UserId",
                table: "TodoTasks",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Todos");

            migrationBuilder.DropTable(
                name: "TodoTasks");
        }
    }
}
