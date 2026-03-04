using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KPIBackend.Migrations
{
    /// <inheritdoc />
    public partial class ActualizacionModeloEventoCalendarioCampoTituloSinAcento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Título",
                table: "eventosCalendario",
                newName: "Titulo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Titulo",
                table: "eventosCalendario",
                newName: "Título");
        }
    }
}
