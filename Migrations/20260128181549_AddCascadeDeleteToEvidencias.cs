using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KPIBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddCascadeDeleteToEvidencias : Migration
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
