using Microsoft.EntityFrameworkCore.Migrations;

namespace Tischtennis_Shop.Migrations
{
    public partial class InitialCreate8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Gesamtpreis",
                table: "Verkaufte_Ware",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Gesamtpreis",
                table: "Verkaufte_Ware",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
