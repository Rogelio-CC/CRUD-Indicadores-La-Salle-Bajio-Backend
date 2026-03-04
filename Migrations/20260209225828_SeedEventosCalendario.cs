using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KPIBackend.Migrations
{
    /// <inheritdoc />
    public partial class SeedEventosCalendario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "eventosCalendario",
                keyColumn: "Id",
                keyValue: new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d470"));

            migrationBuilder.DeleteData(
                table: "eventosCalendario",
                keyColumn: "Id",
                keyValue: new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d479"));

            migrationBuilder.InsertData(
                table: "eventosCalendario",
                columns: new[] { "Id", "Color", "FechaFin", "FechaInicio", "TipoEvento", "Titulo" },
                values: new object[,]
                {
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d475"), "#BFBFBF", new DateTime(2026, 6, 27, 6, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 2, 9, 6, 0, 0, 0, DateTimeKind.Utc), "Académico", "Inicio del semestre escolar" },
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d480"), null, new DateTime(2026, 3, 27, 6, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 20, 6, 0, 0, 0, DateTimeKind.Utc), "Académico", "Semana sin actividad" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "eventosCalendario",
                keyColumn: "Id",
                keyValue: new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d475"));

            migrationBuilder.DeleteData(
                table: "eventosCalendario",
                keyColumn: "Id",
                keyValue: new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d480"));

            migrationBuilder.InsertData(
                table: "eventosCalendario",
                columns: new[] { "Id", "Color", "FechaFin", "FechaInicio", "TipoEvento", "Titulo" },
                values: new object[,]
                {
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d470"), null, new DateTime(2026, 3, 27, 6, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 20, 6, 0, 0, 0, DateTimeKind.Utc), "Académico", "Semana sin actividad" },
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d479"), "#BFBFBF", new DateTime(2026, 6, 27, 6, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 2, 9, 6, 0, 0, 0, DateTimeKind.Utc), "Académico", "Inicio del semestre escolar" }
                });
        }
    }
}
