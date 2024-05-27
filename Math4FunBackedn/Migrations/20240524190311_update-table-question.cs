using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Math4FunBackedn.Migrations
{
    public partial class updatetablequestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "createdAt",
                table: "Question",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "createdAt",
                table: "Question");
        }
    }
}
