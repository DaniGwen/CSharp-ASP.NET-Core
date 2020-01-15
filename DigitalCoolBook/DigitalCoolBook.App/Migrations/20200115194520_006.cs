using Microsoft.EntityFrameworkCore.Migrations;

namespace DigitalCoolBook.App.Migrations
{
    public partial class _006 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Students_IdStudent",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_GradeParalelos_Grades_IdGrade",
                table: "GradeParalelos");

            migrationBuilder.DropForeignKey(
                name: "FK_GradeParalelos_Teachers_IdTeacher",
                table: "GradeParalelos");

            migrationBuilder.DropForeignKey(
                name: "FK_ScoreRecords_Students_IdStudent",
                table: "ScoreRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_ScoreRecords_Subjects_IdSubject",
                table: "ScoreRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_GradeParalelos_IdGradeParalelo",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectGrades_Grades_IdGrade",
                table: "SubjectGrades");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectGrades_Subjects_IdSubject",
                table: "SubjectGrades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Teachers",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_SubjectGrades_IdGrade",
                table: "SubjectGrades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Students");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Teachers",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SubjectId",
                table: "Subjects",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "IdSubject",
                table: "SubjectGrades",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "IdGrade",
                table: "SubjectGrades",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "IdGradeParalelo",
                table: "Students",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Students",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IdSubject",
                table: "ScoreRecords",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "IdStudent",
                table: "ScoreRecords",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "GradeId",
                table: "Grades",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "IdTeacher",
                table: "GradeParalelos",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "IdGrade",
                table: "GradeParalelos",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "GradeParaleloId",
                table: "GradeParalelos",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "IdStudent",
                table: "Attendances",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "AttendanceId",
                table: "Attendances",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Teachers",
                table: "Teachers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectGrades_IdGrade",
                table: "SubjectGrades",
                column: "IdGrade",
                unique: true,
                filter: "[IdGrade] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Students_IdStudent",
                table: "Attendances",
                column: "IdStudent",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GradeParalelos_Grades_IdGrade",
                table: "GradeParalelos",
                column: "IdGrade",
                principalTable: "Grades",
                principalColumn: "GradeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GradeParalelos_Teachers_IdTeacher",
                table: "GradeParalelos",
                column: "IdTeacher",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ScoreRecords_Students_IdStudent",
                table: "ScoreRecords",
                column: "IdStudent",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ScoreRecords_Subjects_IdSubject",
                table: "ScoreRecords",
                column: "IdSubject",
                principalTable: "Subjects",
                principalColumn: "SubjectId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_GradeParalelos_IdGradeParalelo",
                table: "Students",
                column: "IdGradeParalelo",
                principalTable: "GradeParalelos",
                principalColumn: "GradeParaleloId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectGrades_Grades_IdGrade",
                table: "SubjectGrades",
                column: "IdGrade",
                principalTable: "Grades",
                principalColumn: "GradeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectGrades_Subjects_IdSubject",
                table: "SubjectGrades",
                column: "IdSubject",
                principalTable: "Subjects",
                principalColumn: "SubjectId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Students_IdStudent",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_GradeParalelos_Grades_IdGrade",
                table: "GradeParalelos");

            migrationBuilder.DropForeignKey(
                name: "FK_GradeParalelos_Teachers_IdTeacher",
                table: "GradeParalelos");

            migrationBuilder.DropForeignKey(
                name: "FK_ScoreRecords_Students_IdStudent",
                table: "ScoreRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_ScoreRecords_Subjects_IdSubject",
                table: "ScoreRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_GradeParalelos_IdGradeParalelo",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectGrades_Grades_IdGrade",
                table: "SubjectGrades");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectGrades_Subjects_IdSubject",
                table: "SubjectGrades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Teachers",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_SubjectGrades_IdGrade",
                table: "SubjectGrades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "Teachers",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "SubjectId",
                table: "Subjects",
                type: "int",
                nullable: false,
                oldClrType: typeof(string))
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "IdSubject",
                table: "SubjectGrades",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdGrade",
                table: "SubjectGrades",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdGradeParalelo",
                table: "Students",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "IdSubject",
                table: "ScoreRecords",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdStudent",
                table: "ScoreRecords",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GradeId",
                table: "Grades",
                type: "int",
                nullable: false,
                oldClrType: typeof(string))
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "IdTeacher",
                table: "GradeParalelos",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdGrade",
                table: "GradeParalelos",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GradeParaleloId",
                table: "GradeParalelos",
                type: "int",
                nullable: false,
                oldClrType: typeof(string))
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "IdStudent",
                table: "Attendances",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AttendanceId",
                table: "Attendances",
                type: "int",
                nullable: false,
                oldClrType: typeof(string))
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Teachers",
                table: "Teachers",
                column: "TeacherId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectGrades_IdGrade",
                table: "SubjectGrades",
                column: "IdGrade",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Students_IdStudent",
                table: "Attendances",
                column: "IdStudent",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GradeParalelos_Grades_IdGrade",
                table: "GradeParalelos",
                column: "IdGrade",
                principalTable: "Grades",
                principalColumn: "GradeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GradeParalelos_Teachers_IdTeacher",
                table: "GradeParalelos",
                column: "IdTeacher",
                principalTable: "Teachers",
                principalColumn: "TeacherId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScoreRecords_Students_IdStudent",
                table: "ScoreRecords",
                column: "IdStudent",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScoreRecords_Subjects_IdSubject",
                table: "ScoreRecords",
                column: "IdSubject",
                principalTable: "Subjects",
                principalColumn: "SubjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_GradeParalelos_IdGradeParalelo",
                table: "Students",
                column: "IdGradeParalelo",
                principalTable: "GradeParalelos",
                principalColumn: "GradeParaleloId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectGrades_Grades_IdGrade",
                table: "SubjectGrades",
                column: "IdGrade",
                principalTable: "Grades",
                principalColumn: "GradeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectGrades_Subjects_IdSubject",
                table: "SubjectGrades",
                column: "IdSubject",
                principalTable: "Subjects",
                principalColumn: "SubjectId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
