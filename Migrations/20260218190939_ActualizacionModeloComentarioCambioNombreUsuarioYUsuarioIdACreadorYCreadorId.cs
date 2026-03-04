using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KPIBackend.Migrations
{
    /// <inheritdoc />
    public partial class ActualizacionModeloComentarioCambioNombreUsuarioYUsuarioIdACreadorYCreadorId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comentarios_usuarios_UsuarioId",
                table: "comentarios");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "comentarios",
                newName: "CreadorId");

            migrationBuilder.RenameIndex(
                name: "IX_comentarios_UsuarioId",
                table: "comentarios",
                newName: "IX_comentarios_CreadorId");

            migrationBuilder.AddForeignKey(
                name: "FK_comentarios_usuarios_CreadorId",
                table: "comentarios",
                column: "CreadorId",
                principalTable: "usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comentarios_usuarios_CreadorId",
                table: "comentarios");

            migrationBuilder.RenameColumn(
                name: "CreadorId",
                table: "comentarios",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_comentarios_CreadorId",
                table: "comentarios",
                newName: "IX_comentarios_UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_comentarios_usuarios_UsuarioId",
                table: "comentarios",
                column: "UsuarioId",
                principalTable: "usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
