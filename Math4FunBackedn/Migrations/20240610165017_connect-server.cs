using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Math4FunBackedn.Migrations
{
    public partial class connectserver : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Friend_UserId",
                table: "Friend",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Friend_User_UserId",
                table: "Friend",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friend_User_UserId",
                table: "Friend");

            migrationBuilder.DropIndex(
                name: "IX_Friend_UserId",
                table: "Friend");
        }
    }
}
