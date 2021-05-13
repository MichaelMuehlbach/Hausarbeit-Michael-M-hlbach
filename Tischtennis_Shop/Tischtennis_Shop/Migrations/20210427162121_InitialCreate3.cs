using Microsoft.EntityFrameworkCore.Migrations;

namespace Tischtennis_Shop.Migrations
{
    public partial class InitialCreate3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BelagID",
                table: "Mitarbeiter",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mitarbeiter_BelagID",
                table: "Mitarbeiter",
                column: "BelagID");

            migrationBuilder.AddForeignKey(
                name: "FK_Mitarbeiter_Belag_BelagID",
                table: "Mitarbeiter",
                column: "BelagID",
                principalTable: "Belag",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mitarbeiter_Belag_BelagID",
                table: "Mitarbeiter");

            migrationBuilder.DropIndex(
                name: "IX_Mitarbeiter_BelagID",
                table: "Mitarbeiter");

            migrationBuilder.DropColumn(
                name: "BelagID",
                table: "Mitarbeiter");
        }
    }
}
