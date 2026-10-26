using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiDiaInhabil.Migrations
{
    /// <inheritdoc />
    public partial class InitialDiaInhabilSchema : Migration
    {
        /// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "DI_DiasInhabiles",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
					Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
					FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
					FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
					EsGeneral = table.Column<bool>(type: "bit", nullable: false),
					Activo = table.Column<bool>(type: "bit", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_DI_DiasInhabiles", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "DI_Sedes",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
					Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
					Activo = table.Column<bool>(type: "bit", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_DI_Sedes", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "DI_DiaInhabilArea",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					IdDiaInhabil = table.Column<int>(type: "int", nullable: false),
					IdArea = table.Column<int>(type: "int", nullable: false),
					TipoAplicacion = table.Column<int>(type: "int", nullable: false),
					Activo = table.Column<bool>(type: "bit", nullable: false),
					FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_DI_DiaInhabilArea", x => x.Id);
					table.ForeignKey(
						name: "FK_DI_DiaInhabilArea_DI_DiasInhabiles_IdDiaInhabil",
						column: x => x.IdDiaInhabil,
						principalTable: "DI_DiasInhabiles",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_DI_DiaInhabilArea_ADM_AREA_IdArea",
						column: x => x.IdArea,
						principalTable: "ADM_AREA",
						principalColumn: "IdArea",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "DI_DiasInhabilesSedes",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					IdDiaInhabil = table.Column<int>(type: "int", nullable: false),
					IdSede = table.Column<int>(type: "int", nullable: false),
					Activo = table.Column<bool>(type: "bit", nullable: false),
					FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_DI_DiasInhabilesSedes", x => x.Id);
					table.ForeignKey(
						name: "FK_DI_DiasInhabilesSedes_DI_DiasInhabiles_IdDiaInhabil",
						column: x => x.IdDiaInhabil,
						principalTable: "DI_DiasInhabiles",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_DI_DiasInhabilesSedes_DI_Sedes_IdSede",
						column: x => x.IdSede,
						principalTable: "DI_Sedes",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateIndex(
				name: "IX_DI_DiaInhabilArea_IdArea",
				table: "DI_DiaInhabilArea",
				column: "IdArea");

			migrationBuilder.CreateIndex(
				name: "IX_DI_DiaInhabilArea_IdDiaInhabil",
				table: "DI_DiaInhabilArea",
				column: "IdDiaInhabil");

			migrationBuilder.CreateIndex(
				name: "IX_DI_DiasInhabilesSedes_IdDiaInhabil",
				table: "DI_DiasInhabilesSedes",
				column: "IdDiaInhabil");

			migrationBuilder.CreateIndex(
				name: "IX_DI_DiasInhabilesSedes_IdSede",
				table: "DI_DiasInhabilesSedes",
				column: "IdSede");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "DI_DiaInhabilArea");

			migrationBuilder.DropTable(
				name: "DI_DiasInhabilesSedes");

			migrationBuilder.DropTable(
				name: "DI_DiasInhabiles");

			migrationBuilder.DropTable(
				name: "DI_Sedes");
		}
    }
}
