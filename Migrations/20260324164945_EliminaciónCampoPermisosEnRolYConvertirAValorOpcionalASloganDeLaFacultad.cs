using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
// Esta migración se utilizo para borrar la columna "Permisos" en roles y ajustar a valor nulo u opcional al campo "Slogan" de carrera.
namespace KPIBackend.Migrations
{
    /// <inheritdoc />
    public partial class EliminaciónCampoPermisosEnRolYConvertirAValorOpcionalASloganDeLaFacultad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Permisos",
                table: "roles");

            migrationBuilder.AlterColumn<string>(
                name: "Slogan",
                table: "facultades",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Permisos",
                table: "roles",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Slogan",
                table: "facultades",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d473"),
                column: "Permisos",
                value: "Puede modificar todas las tablas.");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d481"),
                column: "Permisos",
                value: "Puede crear actividades.");
        }
    }
}
