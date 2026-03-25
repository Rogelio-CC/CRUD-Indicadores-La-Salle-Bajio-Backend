using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

//Esta migración sirvió para agregar la tabla archivoPoliticas para asociar un archivo con políticas de una facultad y eliminar el atributo o campo PoliticaAsociada en Facultad.
//PoliticaAsociada es remplazado por un archivo con políticas que esten relacionadas con una facultad.
namespace KPIBackend.Migrations
{
    /// <inheritdoc />
    public partial class AgregadoDeEntidadArchivoPoliticasYEliminaciónDelCampoPoliticaAsociadaEnFacultad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PoliticaAsociada",
                table: "facultades");

            migrationBuilder.CreateTable(
                name: "archivoPoliticas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    NombreArchivo = table.Column<string>(type: "text", nullable: false),
                    Tipo = table.Column<string>(type: "text", nullable: false),
                    Contenido = table.Column<byte[]>(type: "bytea", nullable: false),
                    FacultadId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_archivoPoliticas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_archivoPoliticas_facultades_FacultadId",
                        column: x => x.FacultadId,
                        principalTable: "facultades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_archivoPoliticas_FacultadId",
                table: "archivoPoliticas",
                column: "FacultadId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "archivoPoliticas");

            migrationBuilder.AddColumn<string>(
                name: "PoliticaAsociada",
                table: "facultades",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "facultades",
                keyColumn: "Id",
                keyValue: new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d476"),
                column: "PoliticaAsociada",
                value: "Política 1: uso ético de la tecnología.");
        }
    }
}
