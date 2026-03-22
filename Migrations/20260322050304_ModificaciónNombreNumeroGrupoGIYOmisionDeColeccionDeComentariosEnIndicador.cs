using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

// Esta migración sirvió para modificar el nombre del numeroGrupo a NumeroGrupo en la entidad Grupo de indicadores y borrar las colecciones o relaciones de comentarios con la entidad Indicador.

namespace KPIBackend.Migrations
{
    /// <inheritdoc />
    public partial class ModificaciónNombreNumeroGrupoGIYOmisionDeColeccionDeComentariosEnIndicador : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comentarios_indicadores_IndicadorId",
                table: "comentarios");

            migrationBuilder.DropIndex(
                name: "IX_comentarios_IndicadorId",
                table: "comentarios");

            migrationBuilder.DropColumn(
                name: "IndicadorId",
                table: "comentarios");

            migrationBuilder.RenameColumn(
                name: "numeroGrupo",
                table: "grupo_indicadores",
                newName: "NumeroGrupo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumeroGrupo",
                table: "grupo_indicadores",
                newName: "numeroGrupo");

            migrationBuilder.AddColumn<Guid>(
                name: "IndicadorId",
                table: "comentarios",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_comentarios_IndicadorId",
                table: "comentarios",
                column: "IndicadorId");

            migrationBuilder.AddForeignKey(
                name: "FK_comentarios_indicadores_IndicadorId",
                table: "comentarios",
                column: "IndicadorId",
                principalTable: "indicadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
