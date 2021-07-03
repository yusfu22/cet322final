using Microsoft.EntityFrameworkCore.Migrations;

namespace cet322finaltodo.Data.Migrations
{
    public partial class vol3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirmUserId",
                table: "todoItems",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_todoItems_FirmUserId",
                table: "todoItems",
                column: "FirmUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_todoItems_AspNetUsers_FirmUserId",
                table: "todoItems",
                column: "FirmUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_todoItems_AspNetUsers_FirmUserId",
                table: "todoItems");

            migrationBuilder.DropIndex(
                name: "IX_todoItems_FirmUserId",
                table: "todoItems");

            migrationBuilder.DropColumn(
                name: "FirmUserId",
                table: "todoItems");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "AspNetUsers");
        }
    }
}
