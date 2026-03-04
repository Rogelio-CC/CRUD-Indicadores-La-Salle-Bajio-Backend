using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KPIBackend.Migrations
{
    /// <inheritdoc />
    public partial class ActualizacionModelosActividadEstrategiaComentariollavesForaneasAlteradas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdCarrera",
                table: "estrategias");

            migrationBuilder.DropColumn(
                name: "IdCreador",
                table: "estrategias");

            migrationBuilder.DropColumn(
                name: "IdIndicador",
                table: "estrategias");

            migrationBuilder.DropColumn(
                name: "IdPeriodo",
                table: "estrategias");

            migrationBuilder.DropColumn(
                name: "IdUsuario",
                table: "comentarios");

            migrationBuilder.DropColumn(
                name: "IdCarrera",
                table: "actividades");

            migrationBuilder.DropColumn(
                name: "IdCreador",
                table: "actividades");

            migrationBuilder.DropColumn(
                name: "IdEstrategia",
                table: "actividades");

            migrationBuilder.DropColumn(
                name: "IdPeriodo",
                table: "actividades");

            migrationBuilder.RenameColumn(
                name: "Cumplida",
                table: "actividades",
                newName: "ActividadCumplida");

            migrationBuilder.AlterColumn<Guid>(
                name: "IdObjetivo",
                table: "comentarios",
                type: "uuid",
                nullable: false,
                defaultValueSql: "gen_random_uuid()",
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "FechaCumplimiento",
                table: "actividades",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ActividadCumplida",
                table: "actividades",
                newName: "Cumplida");

            migrationBuilder.AddColumn<Guid>(
                name: "IdCarrera",
                table: "estrategias",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "IdCreador",
                table: "estrategias",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "IdIndicador",
                table: "estrategias",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "IdPeriodo",
                table: "estrategias",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "IdObjetivo",
                table: "comentarios",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "gen_random_uuid()");

            migrationBuilder.AddColumn<Guid>(
                name: "IdUsuario",
                table: "comentarios",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCumplimiento",
                table: "actividades",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "IdCarrera",
                table: "actividades",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "IdCreador",
                table: "actividades",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "IdEstrategia",
                table: "actividades",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "IdPeriodo",
                table: "actividades",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
