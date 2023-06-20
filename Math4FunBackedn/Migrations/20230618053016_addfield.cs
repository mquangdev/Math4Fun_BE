using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Math4FunBackedn.Migrations
{
    public partial class addfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCorrect",
                table: "Answer");

            migrationBuilder.AddColumn<double>(
                name: "TotalGem",
                table: "User",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Answer",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalGem",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "Answer");

            migrationBuilder.AddColumn<bool>(
                name: "IsCorrect",
                table: "Answer",
                type: "boolean",
                nullable: true);
        }
    }
}
