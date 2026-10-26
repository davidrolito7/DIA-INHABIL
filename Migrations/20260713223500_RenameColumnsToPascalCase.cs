using ApiDiaInhabil.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiDiaInhabil.Migrations
{
    [DbContext(typeof(ApiDiaInhabilContext))]
    [Migration("20260713223500_RenameColumnsToPascalCase")]
    public partial class RenameColumnsToPascalCase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "DI_DiasInhabiles",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "nombre",
                table: "DI_DiasInhabiles",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "descripcion",
                table: "DI_DiasInhabiles",
                newName: "Descripcion");

            migrationBuilder.RenameColumn(
                name: "fechaInicio",
                table: "DI_DiasInhabiles",
                newName: "FechaInicio");

            migrationBuilder.RenameColumn(
                name: "fechaFin",
                table: "DI_DiasInhabiles",
                newName: "FechaFin");

            migrationBuilder.RenameColumn(
                name: "esGeneral",
                table: "DI_DiasInhabiles",
                newName: "EsGeneral");

            migrationBuilder.RenameColumn(
                name: "activo",
                table: "DI_DiasInhabiles",
                newName: "Activo");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "DI_Sedes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "nombre",
                table: "DI_Sedes",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "descripcion",
                table: "DI_Sedes",
                newName: "Descripcion");

            migrationBuilder.RenameColumn(
                name: "activo",
                table: "DI_Sedes",
                newName: "Activo");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "DI_DiaInhabilArea",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "idDiaInhabil",
                table: "DI_DiaInhabilArea",
                newName: "IdDiaInhabil");

            migrationBuilder.RenameColumn(
                name: "idArea",
                table: "DI_DiaInhabilArea",
                newName: "IdArea");

            migrationBuilder.RenameColumn(
                name: "tipoAplicacion",
                table: "DI_DiaInhabilArea",
                newName: "TipoAplicacion");

            migrationBuilder.RenameColumn(
                name: "activo",
                table: "DI_DiaInhabilArea",
                newName: "Activo");

            migrationBuilder.RenameColumn(
                name: "fechaRegistro",
                table: "DI_DiaInhabilArea",
                newName: "FechaRegistro");

            migrationBuilder.RenameIndex(
                name: "IX_DI_DiaInhabilArea_idArea",
                table: "DI_DiaInhabilArea",
                newName: "IX_DI_DiaInhabilArea_IdArea");

            migrationBuilder.RenameIndex(
                name: "IX_DI_DiaInhabilArea_idDiaInhabil",
                table: "DI_DiaInhabilArea",
                newName: "IX_DI_DiaInhabilArea_IdDiaInhabil");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "DI_DiasInhabilesSedes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "idDiaInhabil",
                table: "DI_DiasInhabilesSedes",
                newName: "IdDiaInhabil");

            migrationBuilder.RenameColumn(
                name: "idSede",
                table: "DI_DiasInhabilesSedes",
                newName: "IdSede");

            migrationBuilder.RenameColumn(
                name: "activo",
                table: "DI_DiasInhabilesSedes",
                newName: "Activo");

            migrationBuilder.RenameColumn(
                name: "fechaRegistro",
                table: "DI_DiasInhabilesSedes",
                newName: "FechaRegistro");

            migrationBuilder.RenameIndex(
                name: "IX_DI_DiasInhabilesSedes_idDiaInhabil",
                table: "DI_DiasInhabilesSedes",
                newName: "IX_DI_DiasInhabilesSedes_IdDiaInhabil");

            migrationBuilder.RenameIndex(
                name: "IX_DI_DiasInhabilesSedes_idSede",
                table: "DI_DiasInhabilesSedes",
                newName: "IX_DI_DiasInhabilesSedes_IdSede");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "DI_DiasInhabiles",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "DI_DiasInhabiles",
                newName: "nombre");

            migrationBuilder.RenameColumn(
                name: "Descripcion",
                table: "DI_DiasInhabiles",
                newName: "descripcion");

            migrationBuilder.RenameColumn(
                name: "FechaInicio",
                table: "DI_DiasInhabiles",
                newName: "fechaInicio");

            migrationBuilder.RenameColumn(
                name: "FechaFin",
                table: "DI_DiasInhabiles",
                newName: "fechaFin");

            migrationBuilder.RenameColumn(
                name: "EsGeneral",
                table: "DI_DiasInhabiles",
                newName: "esGeneral");

            migrationBuilder.RenameColumn(
                name: "Activo",
                table: "DI_DiasInhabiles",
                newName: "activo");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "DI_Sedes",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "DI_Sedes",
                newName: "nombre");

            migrationBuilder.RenameColumn(
                name: "Descripcion",
                table: "DI_Sedes",
                newName: "descripcion");

            migrationBuilder.RenameColumn(
                name: "Activo",
                table: "DI_Sedes",
                newName: "activo");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "DI_DiaInhabilArea",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "IdDiaInhabil",
                table: "DI_DiaInhabilArea",
                newName: "idDiaInhabil");

            migrationBuilder.RenameColumn(
                name: "IdArea",
                table: "DI_DiaInhabilArea",
                newName: "idArea");

            migrationBuilder.RenameColumn(
                name: "TipoAplicacion",
                table: "DI_DiaInhabilArea",
                newName: "tipoAplicacion");

            migrationBuilder.RenameColumn(
                name: "Activo",
                table: "DI_DiaInhabilArea",
                newName: "activo");

            migrationBuilder.RenameColumn(
                name: "FechaRegistro",
                table: "DI_DiaInhabilArea",
                newName: "fechaRegistro");

            migrationBuilder.RenameIndex(
                name: "IX_DI_DiaInhabilArea_IdArea",
                table: "DI_DiaInhabilArea",
                newName: "IX_DI_DiaInhabilArea_idArea");

            migrationBuilder.RenameIndex(
                name: "IX_DI_DiaInhabilArea_IdDiaInhabil",
                table: "DI_DiaInhabilArea",
                newName: "IX_DI_DiaInhabilArea_idDiaInhabil");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "DI_DiasInhabilesSedes",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "IdDiaInhabil",
                table: "DI_DiasInhabilesSedes",
                newName: "idDiaInhabil");

            migrationBuilder.RenameColumn(
                name: "IdSede",
                table: "DI_DiasInhabilesSedes",
                newName: "idSede");

            migrationBuilder.RenameColumn(
                name: "Activo",
                table: "DI_DiasInhabilesSedes",
                newName: "activo");

            migrationBuilder.RenameColumn(
                name: "FechaRegistro",
                table: "DI_DiasInhabilesSedes",
                newName: "fechaRegistro");

            migrationBuilder.RenameIndex(
                name: "IX_DI_DiasInhabilesSedes_IdDiaInhabil",
                table: "DI_DiasInhabilesSedes",
                newName: "IX_DI_DiasInhabilesSedes_idDiaInhabil");

            migrationBuilder.RenameIndex(
                name: "IX_DI_DiasInhabilesSedes_IdSede",
                table: "DI_DiasInhabilesSedes",
                newName: "IX_DI_DiasInhabilesSedes_idSede");
        }
    }
}
