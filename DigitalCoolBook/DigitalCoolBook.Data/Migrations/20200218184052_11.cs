using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DigitalCoolBook.Data.Migrations
{
    public partial class _11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Timer",
                table: "Tests",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Timer",
                table: "Tests",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime));
        }
    }
}
