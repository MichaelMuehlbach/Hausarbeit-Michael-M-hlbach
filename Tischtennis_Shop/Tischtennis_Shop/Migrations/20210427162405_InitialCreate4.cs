﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Tischtennis_Shop.Migrations
{
    public partial class InitialCreate4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "MitarbeiterID",
                table: "Belag",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Belag_MitarbeiterID",
                table: "Belag",
                column: "MitarbeiterID");

            migrationBuilder.AddForeignKey(
                name: "FK_Belag_Mitarbeiter_MitarbeiterID",
                table: "Belag",
                column: "MitarbeiterID",
                principalTable: "Mitarbeiter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Belag_Mitarbeiter_MitarbeiterID",
                table: "Belag");

            migrationBuilder.DropIndex(
                name: "IX_Belag_MitarbeiterID",
                table: "Belag");

            migrationBuilder.DropColumn(
                name: "MitarbeiterID",
                table: "Belag");

            migrationBuilder.AddColumn<int>(
                name: "BelagID",
                table: "Mitarbeiter",
                type: "int",
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
    }
}
