using Microsoft.EntityFrameworkCore.Migrations;

namespace Tischtennis_Shop.Migrations
{
    public partial class InitialCreate9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Salt",
                table: "Mitarbeiter",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salt",
                table: "Mitarbeiter");
        }
    }
}
