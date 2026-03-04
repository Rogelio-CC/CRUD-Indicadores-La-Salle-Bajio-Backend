using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KPIBackend.Migrations
{
    /// <inheritdoc />
    public partial class ActualizacionModelosFacultadPeriodoEscolarRolCambioNombreFacultadNombrePeriodoNombreRolANombre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NombreRol",
                table: "roles",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "NombrePeriodo",
                table: "periodos_escolares",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "NombreFacultad",
                table: "facultades",
                newName: "Nombre");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "roles",
                newName: "NombreRol");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "periodos_escolares",
                newName: "NombrePeriodo");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "facultades",
                newName: "NombreFacultad");
        }
    }
}
