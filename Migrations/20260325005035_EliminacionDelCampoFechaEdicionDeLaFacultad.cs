using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

//Esta migración sirvió para eliminar el campo FechaEdición de la entidad Facultad por conflictos con el archivo de política asociado.
namespace KPIBackend.Migrations
{
    /// <inheritdoc />
    public partial class EliminacionDelCampoFechaEdicionDeLaFacultad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaEdicion",
                table: "facultades");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaEdicion",
                table: "facultades",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "facultades",
                keyColumn: "Id",
                keyValue: new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d476"),
                column: "FechaEdicion",
                value: null);
        }
    }
}
