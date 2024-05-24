using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Math4FunBackedn.Migrations
{
    public partial class updatetablequestions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isAiGen",
                table: "Question",
                type: "boolean",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isAiGen",
                table: "Question");
        }
    }
}
