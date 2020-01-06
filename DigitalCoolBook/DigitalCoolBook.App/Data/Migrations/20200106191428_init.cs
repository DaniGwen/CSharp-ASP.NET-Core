using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DigitalCoolBook.App.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    GradeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.GradeId);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    SubjectId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: true),
                    Abbreviation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.SubjectId);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    TeacherId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    PlaceOfBirth = table.Column<string>(maxLength: 100, nullable: true),
                    Sex = table.Column<string>(maxLength: 20, nullable: true),
                    MobilePhone = table.Column<int>(nullable: false),
                    Telephone = table.Column<int>(nullable: false),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.TeacherId);
                });

            migrationBuilder.CreateTable(
                name: "SubjectGrades",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdGrade = table.Column<int>(nullable: false),
                    IdSubject = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectGrades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubjectGrades_Grades_IdGrade",
                        column: x => x.IdGrade,
                        principalTable: "Grades",
                        principalColumn: "GradeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectGrades_Subjects_IdSubject",
                        column: x => x.IdSubject,
                        principalTable: "Subjects",
                        principalColumn: "SubjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GradeParalelos",
                columns: table => new
                {
                    GradeParaleloId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    IdGrade = table.Column<int>(nullable: false),
                    IdTeacher = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeParalelos", x => x.GradeParaleloId);
                    table.ForeignKey(
                        name: "FK_GradeParalelos_Grades_IdGrade",
                        column: x => x.IdGrade,
                        principalTable: "Grades",
                        principalColumn: "GradeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GradeParalelos_Teachers_IdTeacher",
                        column: x => x.IdTeacher,
                        principalTable: "Teachers",
                        principalColumn: "TeacherId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    PlaceOfBirth = table.Column<string>(maxLength: 200, nullable: true),
                    Sex = table.Column<string>(maxLength: 20, nullable: true),
                    MobilePhone = table.Column<int>(maxLength: 20, nullable: false),
                    Address = table.Column<string>(maxLength: 100, nullable: true),
                    FatherName = table.Column<string>(maxLength: 50, nullable: true),
                    MotherName = table.Column<string>(maxLength: 50, nullable: true),
                    MotherMobileNumber = table.Column<int>(nullable: false),
                    FatherMobileNumber = table.Column<int>(nullable: false),
                    Telephone = table.Column<int>(nullable: false),
                    Password = table.Column<string>(nullable: true),
                    IdGradeParalelo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_Students_GradeParalelos_IdGradeParalelo",
                        column: x => x.IdGradeParalelo,
                        principalTable: "GradeParalelos",
                        principalColumn: "GradeParaleloId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    AttendanceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    Attended = table.Column<bool>(nullable: false),
                    IdStudent = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => x.AttendanceId);
                    table.ForeignKey(
                        name: "FK_Attendances_Students_IdStudent",
                        column: x => x.IdStudent,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScoreRecords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSubject = table.Column<int>(nullable: false),
                    IdStudent = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoreRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScoreRecords_Students_IdStudent",
                        column: x => x.IdStudent,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScoreRecords_Subjects_IdSubject",
                        column: x => x.IdSubject,
                        principalTable: "Subjects",
                        principalColumn: "SubjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_IdStudent",
                table: "Attendances",
                column: "IdStudent");

            migrationBuilder.CreateIndex(
                name: "IX_GradeParalelos_IdGrade",
                table: "GradeParalelos",
                column: "IdGrade");

            migrationBuilder.CreateIndex(
                name: "IX_GradeParalelos_IdTeacher",
                table: "GradeParalelos",
                column: "IdTeacher");

            migrationBuilder.CreateIndex(
                name: "IX_ScoreRecords_IdStudent",
                table: "ScoreRecords",
                column: "IdStudent");

            migrationBuilder.CreateIndex(
                name: "IX_ScoreRecords_IdSubject",
                table: "ScoreRecords",
                column: "IdSubject");

            migrationBuilder.CreateIndex(
                name: "IX_Students_IdGradeParalelo",
                table: "Students",
                column: "IdGradeParalelo");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectGrades_IdGrade",
                table: "SubjectGrades",
                column: "IdGrade",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubjectGrades_IdSubject",
                table: "SubjectGrades",
                column: "IdSubject");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendances");

            migrationBuilder.DropTable(
                name: "ScoreRecords");

            migrationBuilder.DropTable(
                name: "SubjectGrades");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "GradeParalelos");

            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropTable(
                name: "Teachers");
        }
    }
}
