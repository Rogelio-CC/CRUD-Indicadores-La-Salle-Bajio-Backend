using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

// Esta migración sirvió para agregar relación foránea entre un comentario a una actividad, estrategias o directriz (cada una de esta tres tiene un atributo Id del comentario). Se eliminaron colecciones en c#
// para evitar doble redundancia de relación entre esas entidades antes mencionadas.
namespace KPIBackend.Migrations
{
    /// <inheritdoc />
    public partial class ModificacionRelacionOpcionalActividadEstrategiaDirectrizConComentarioPorMedioDelIdDeComentario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comentarios_actividades_ActividadId",
                table: "comentarios");

            migrationBuilder.DropForeignKey(
                name: "FK_comentarios_directrices_DirectrizId",
                table: "comentarios");

            migrationBuilder.DropForeignKey(
                name: "FK_comentarios_estrategias_EstrategiaId",
                table: "comentarios");

            migrationBuilder.DropIndex(
                name: "IX_comentarios_ActividadId",
                table: "comentarios");

            migrationBuilder.DropIndex(
                name: "IX_comentarios_DirectrizId",
                table: "comentarios");

            migrationBuilder.DropIndex(
                name: "IX_comentarios_EstrategiaId",
                table: "comentarios");

            migrationBuilder.DropColumn(
                name: "ActividadId",
                table: "comentarios");

            migrationBuilder.DropColumn(
                name: "DirectrizId",
                table: "comentarios");

            migrationBuilder.DropColumn(
                name: "EstrategiaId",
                table: "comentarios");

            migrationBuilder.AddColumn<Guid>(
                name: "ComentarioId",
                table: "estrategias",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ComentarioId",
                table: "directrices",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ComentarioId",
                table: "actividades",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_estrategias_ComentarioId",
                table: "estrategias",
                column: "ComentarioId");

            migrationBuilder.CreateIndex(
                name: "IX_directrices_ComentarioId",
                table: "directrices",
                column: "ComentarioId");

            migrationBuilder.CreateIndex(
                name: "IX_actividades_ComentarioId",
                table: "actividades",
                column: "ComentarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_actividades_comentarios_ComentarioId",
                table: "actividades",
                column: "ComentarioId",
                principalTable: "comentarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_directrices_comentarios_ComentarioId",
                table: "directrices",
                column: "ComentarioId",
                principalTable: "comentarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_estrategias_comentarios_ComentarioId",
                table: "estrategias",
                column: "ComentarioId",
                principalTable: "comentarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_actividades_comentarios_ComentarioId",
                table: "actividades");

            migrationBuilder.DropForeignKey(
                name: "FK_directrices_comentarios_ComentarioId",
                table: "directrices");

            migrationBuilder.DropForeignKey(
                name: "FK_estrategias_comentarios_ComentarioId",
                table: "estrategias");

            migrationBuilder.DropIndex(
                name: "IX_estrategias_ComentarioId",
                table: "estrategias");

            migrationBuilder.DropIndex(
                name: "IX_directrices_ComentarioId",
                table: "directrices");

            migrationBuilder.DropIndex(
                name: "IX_actividades_ComentarioId",
                table: "actividades");

            migrationBuilder.DropColumn(
                name: "ComentarioId",
                table: "estrategias");

            migrationBuilder.DropColumn(
                name: "ObjetivoId",
                table: "estrategias");

            migrationBuilder.DropColumn(
                name: "ComentarioId",
                table: "directrices");

            migrationBuilder.DropColumn(
                name: "ObjetivoId",
                table: "directrices");

            migrationBuilder.DropColumn(
                name: "ComentarioId",
                table: "actividades");

            migrationBuilder.DropColumn(
                name: "ObjetivoId",
                table: "actividades");

            migrationBuilder.AddColumn<Guid>(
                name: "ActividadId",
                table: "comentarios",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DirectrizId",
                table: "comentarios",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EstrategiaId",
                table: "comentarios",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_comentarios_ActividadId",
                table: "comentarios",
                column: "ActividadId");

            migrationBuilder.CreateIndex(
                name: "IX_comentarios_DirectrizId",
                table: "comentarios",
                column: "DirectrizId");

            migrationBuilder.CreateIndex(
                name: "IX_comentarios_EstrategiaId",
                table: "comentarios",
                column: "EstrategiaId");

            migrationBuilder.AddForeignKey(
                name: "FK_comentarios_actividades_ActividadId",
                table: "comentarios",
                column: "ActividadId",
                principalTable: "actividades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_comentarios_directrices_DirectrizId",
                table: "comentarios",
                column: "DirectrizId",
                principalTable: "directrices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_comentarios_estrategias_EstrategiaId",
                table: "comentarios",
                column: "EstrategiaId",
                principalTable: "estrategias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

        }
    }
}
