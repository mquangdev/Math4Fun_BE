using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Math4FunBackedn.Migrations
{
    public partial class addcreatedDateField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Lesson",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Chapter",
                type: "timestamp with time zone",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Lesson");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Chapter");
        }
    }
}
