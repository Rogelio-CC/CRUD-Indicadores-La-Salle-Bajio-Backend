using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

// Esta migración se utilizo para eliminar la tabla EventosCalendario y aumentar el número de caracteres para visión y misión de la facultad.
namespace KPIBackend.Migrations
{
    /// <inheritdoc />
    public partial class EliminacionDeLaTablaEventosCalendarioYAumentoDeNumeroDecaracteresParaMisionYVisionDeLaFacultad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "eventosCalendario");

            migrationBuilder.AlterColumn<string>(
                name: "Vision",
                table: "facultades",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Mision",
                table: "facultades",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Vision",
                table: "facultades",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "Mision",
                table: "facultades",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000);

            migrationBuilder.CreateTable(
                name: "eventosCalendario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Color = table.Column<string>(type: "text", nullable: true),
                    FechaFin = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TipoEvento = table.Column<string>(type: "text", nullable: false),
                    Titulo = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eventosCalendario", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "eventosCalendario",
                columns: new[] { "Id", "Color", "FechaFin", "FechaInicio", "TipoEvento", "Titulo" },
                values: new object[,]
                {
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d475"), "#BFBFBF", new DateTime(2026, 6, 27, 6, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 2, 9, 6, 0, 0, 0, DateTimeKind.Utc), "Académico", "Inicio del semestre escolar" },
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d480"), null, new DateTime(2026, 3, 27, 6, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 20, 6, 0, 0, 0, DateTimeKind.Utc), "Académico", "Semana sin actividad" }
                });
        }
    }
}
