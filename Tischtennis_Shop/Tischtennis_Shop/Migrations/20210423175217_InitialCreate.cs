using Microsoft.EntityFrameworkCore.Migrations;

namespace Tischtennis_Shop.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Belag",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Belag_Art = table.Column<string>(nullable: true),
                    Menge_Schwarz = table.Column<int>(nullable: false),
                    Menge_Rot = table.Column<int>(nullable: false),
                    Marke = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Preis = table.Column<decimal>(nullable: false),
                    Bildpfad = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Belag", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Belag");
        }
    }
}
