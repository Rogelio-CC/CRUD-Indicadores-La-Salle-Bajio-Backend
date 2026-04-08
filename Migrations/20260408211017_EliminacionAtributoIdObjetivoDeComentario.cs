using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

//Esta migración sirvió para eliminar el atributo IdObjetivo de la entidad Comentario, ya que no era necesario para la funcionalidad del sistema. Además, si se dejaba ese atributo, hubiera existido confuciones entre Id (llave primaria del comentario) y IdObjetivo en la parte de las consultas.

namespace KPIBackend.Migrations
{
    /// <inheritdoc />
    public partial class EliminacionAtributoIdObjetivoDeComentario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdObjetivo",
                table: "comentarios");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IdObjetivo",
                table: "comentarios",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()");
        }
    }
}
