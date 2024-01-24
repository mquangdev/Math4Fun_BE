using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Math4FunBackedn.Migrations
{
    public partial class AddFieldOfQuestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TextBonus",
                table: "Question",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TextBonus",
                table: "Question");
        }
    }
}
