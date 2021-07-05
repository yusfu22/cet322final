using Microsoft.EntityFrameworkCore.Migrations;

namespace cet322finaltodo.Data.Migrations
{
    public partial class statustocrew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "AspNetUsers",
                newName: "Crew");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Crew",
                table: "AspNetUsers",
                newName: "Status");
        }
    }
}
