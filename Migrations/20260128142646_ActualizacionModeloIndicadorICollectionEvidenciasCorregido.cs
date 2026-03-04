using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KPIBackend.Migrations
{
    /// <inheritdoc />
    public partial class ActualizacionModeloIndicadorICollectionEvidenciasCorregido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_evidencias_evidencias_EvidenciaId",
                table: "evidencias");

            migrationBuilder.DropIndex(
                name: "IX_evidencias_EvidenciaId",
                table: "evidencias");

            migrationBuilder.DropColumn(
                name: "EvidenciaId",
                table: "evidencias");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EvidenciaId",
                table: "evidencias",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_evidencias_EvidenciaId",
                table: "evidencias",
                column: "EvidenciaId");

            migrationBuilder.AddForeignKey(
                name: "FK_evidencias_evidencias_EvidenciaId",
                table: "evidencias",
                column: "EvidenciaId",
                principalTable: "evidencias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
