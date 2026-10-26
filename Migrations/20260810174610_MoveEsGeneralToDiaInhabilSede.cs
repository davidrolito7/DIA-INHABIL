using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiDiaInhabil.Migrations
{
    /// <inheritdoc />
    public partial class MoveEsGeneralToDiaInhabilSede : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EsGeneral",
                table: "DI_DiasInhabilesSedes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.Sql("""
                UPDATE diaSede
                SET EsGeneral = dia.EsGeneral
                FROM DI_DiasInhabilesSedes AS diaSede
                INNER JOIN DI_DiasInhabiles AS dia ON dia.Id = diaSede.IdDiaInhabil;
                """);

            migrationBuilder.DropColumn(
                name: "EsGeneral",
                table: "DI_DiasInhabiles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EsGeneral",
                table: "DI_DiasInhabiles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.Sql("""
                UPDATE dia
                SET EsGeneral = 1
                FROM DI_DiasInhabiles AS dia
                WHERE EXISTS (
                    SELECT 1
                    FROM DI_DiasInhabilesSedes AS diaSede
                    WHERE diaSede.IdDiaInhabil = dia.Id
                      AND diaSede.EsGeneral = 1
                );
                """);

            migrationBuilder.DropColumn(
                name: "EsGeneral",
                table: "DI_DiasInhabilesSedes");
        }
    }
}
