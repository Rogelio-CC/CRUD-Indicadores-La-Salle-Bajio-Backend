using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

// Esta migración sirvió para habilitar la eliminación Cascada de una evidencia cuando se borra un indicador.
namespace KPIBackend.Migrations
{
    /// <inheritdoc />
    public partial class EliminacionCascadaEvidenciaEnIndicador : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_evidencias_indicadores_IndicadorId",
                table: "evidencias");

            migrationBuilder.AddForeignKey(
                name: "FK_evidencias_indicadores_IndicadorId",
                table: "evidencias",
                column: "IndicadorId",
                principalTable: "indicadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_evidencias_indicadores_IndicadorId",
                table: "evidencias");

            migrationBuilder.AddForeignKey(
                name: "FK_evidencias_indicadores_IndicadorId",
                table: "evidencias",
                column: "IndicadorId",
                principalTable: "indicadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
