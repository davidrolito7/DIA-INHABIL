using ApiDiaInhabil.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiDiaInhabil.Migrations;

[DbContext(typeof(ApiDiaInhabilContext))]
[Migration("20260810180000_RelateDiaInhabilAreaToDiaInhabilSede")]
public partial class RelateDiaInhabilAreaToDiaInhabilSede : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_DI_DiaInhabilArea_DI_DiasInhabiles_IdDiaInhabil",
            table: "DI_DiaInhabilArea");

        migrationBuilder.RenameColumn(
            name: "IdDiaInhabil",
            table: "DI_DiaInhabilArea",
            newName: "IdDiaInhabilSede");

        migrationBuilder.RenameIndex(
            name: "IX_DI_DiaInhabilArea_IdDiaInhabil",
            table: "DI_DiaInhabilArea",
            newName: "IX_DI_DiaInhabilArea_IdDiaInhabilSede");

        migrationBuilder.AddForeignKey(
            name: "FK_DI_DiaInhabilArea_DI_DiasInhabilesSedes_IdDiaInhabilSede",
            table: "DI_DiaInhabilArea",
            column: "IdDiaInhabilSede",
            principalTable: "DI_DiasInhabilesSedes",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_DI_DiaInhabilArea_DI_DiasInhabilesSedes_IdDiaInhabilSede",
            table: "DI_DiaInhabilArea");

        migrationBuilder.RenameColumn(
            name: "IdDiaInhabilSede",
            table: "DI_DiaInhabilArea",
            newName: "IdDiaInhabil");

        migrationBuilder.RenameIndex(
            name: "IX_DI_DiaInhabilArea_IdDiaInhabilSede",
            table: "DI_DiaInhabilArea",
            newName: "IX_DI_DiaInhabilArea_IdDiaInhabil");

        migrationBuilder.AddForeignKey(
            name: "FK_DI_DiaInhabilArea_DI_DiasInhabiles_IdDiaInhabil",
            table: "DI_DiaInhabilArea",
            column: "IdDiaInhabil",
            principalTable: "DI_DiasInhabiles",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict);
    }
}
