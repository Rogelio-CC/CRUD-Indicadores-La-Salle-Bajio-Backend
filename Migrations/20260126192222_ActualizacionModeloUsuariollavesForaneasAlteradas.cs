using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KPIBackend.Migrations
{
    /// <inheritdoc />
    public partial class ActualizacionModeloUsuariollavesForaneasAlteradas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdCarrera",
                table: "usuarios");

            migrationBuilder.DropColumn(
                name: "IdFacultad",
                table: "usuarios");

            migrationBuilder.DropColumn(
                name: "IdRol",
                table: "usuarios");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IdCarrera",
                table: "usuarios",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "IdFacultad",
                table: "usuarios",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "IdRol",
                table: "usuarios",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
