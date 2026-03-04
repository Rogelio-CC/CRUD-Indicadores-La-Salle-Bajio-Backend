using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KPIBackend.Migrations
{
    /// <inheritdoc />
    public partial class ActualizacionModeloDirectrizllavesForaneasAlteradas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdCreador",
                table: "directrices");

            migrationBuilder.DropColumn(
                name: "IdFacultad",
                table: "directrices");

            migrationBuilder.DropColumn(
                name: "IdPeriodo",
                table: "directrices");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IdCreador",
                table: "directrices",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "IdFacultad",
                table: "directrices",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "IdPeriodo",
                table: "directrices",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
