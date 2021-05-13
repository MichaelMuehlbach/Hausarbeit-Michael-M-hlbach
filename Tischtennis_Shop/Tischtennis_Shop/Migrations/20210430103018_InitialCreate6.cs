using Microsoft.EntityFrameworkCore.Migrations;

namespace Tischtennis_Shop.Migrations
{
    public partial class InitialCreate6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kunde",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vorname = table.Column<string>(nullable: true),
                    Nachname = table.Column<string>(nullable: true),
                    Straße = table.Column<string>(nullable: true),
                    Ort = table.Column<string>(nullable: true),
                    Postleitzahl = table.Column<int>(nullable: false),
                    Hausnummer = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kunde", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Rechnung",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Gesamtbetrag = table.Column<decimal>(nullable: false),
                    KundeID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rechnung", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Rechnung_Kunde_KundeID",
                        column: x => x.KundeID,
                        principalTable: "Kunde",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Verkaufte_Ware",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Farbe = table.Column<string>(nullable: true),
                    Menge = table.Column<int>(nullable: false),
                    Preis_je_Stueck = table.Column<decimal>(nullable: false),
                    RechnungID = table.Column<int>(nullable: true),
                    BelagID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Verkaufte_Ware", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Verkaufte_Ware_Belag_BelagID",
                        column: x => x.BelagID,
                        principalTable: "Belag",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Verkaufte_Ware_Rechnung_RechnungID",
                        column: x => x.RechnungID,
                        principalTable: "Rechnung",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rechnung_KundeID",
                table: "Rechnung",
                column: "KundeID");

            migrationBuilder.CreateIndex(
                name: "IX_Verkaufte_Ware_BelagID",
                table: "Verkaufte_Ware",
                column: "BelagID");

            migrationBuilder.CreateIndex(
                name: "IX_Verkaufte_Ware_RechnungID",
                table: "Verkaufte_Ware",
                column: "RechnungID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Verkaufte_Ware");

            migrationBuilder.DropTable(
                name: "Rechnung");

            migrationBuilder.DropTable(
                name: "Kunde");
        }
    }
}
