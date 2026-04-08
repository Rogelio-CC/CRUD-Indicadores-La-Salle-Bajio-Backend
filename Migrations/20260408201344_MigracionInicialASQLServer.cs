using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

// Esta migración inicial crea la estructura de la base de datos para la aplicación, incluyendo tablas, relaciones y datos en sql server.

namespace KPIBackend.Migrations
{
    /// <inheritdoc />
    public partial class MigracionInicialASQLServer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "facultades",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Mision = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Vision = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Slogan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaEmision = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_facultades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "grupo_indicadores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    NumeroGrupo = table.Column<int>(type: "int", nullable: false),
                    DescripcionGrupo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_grupo_indicadores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "periodos_escolares",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FechaInicio = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    FechaFin = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_periodos_escolares", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "archivoPoliticas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    NombreArchivo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contenido = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    FacultadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "carreras",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    NombreCarrera = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacultadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carreras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_carreras_facultades_FacultadId",
                        column: x => x.FacultadId,
                        principalTable: "facultades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    NombreUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorreoInstitucional = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RolId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacultadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CarreraId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_usuarios_carreras_CarreraId",
                        column: x => x.CarreraId,
                        principalTable: "carreras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_usuarios_facultades_FacultadId",
                        column: x => x.FacultadId,
                        principalTable: "facultades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_usuarios_roles_RolId",
                        column: x => x.RolId,
                        principalTable: "roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "comentarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Contenido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaComentario = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipoObjetivo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdObjetivo = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    CreadorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comentarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_comentarios_usuarios_CreadorId",
                        column: x => x.CreadorId,
                        principalTable: "usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "directrices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacultadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreadorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PeriodoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComentarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_directrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_directrices_comentarios_ComentarioId",
                        column: x => x.ComentarioId,
                        principalTable: "comentarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_directrices_facultades_FacultadId",
                        column: x => x.FacultadId,
                        principalTable: "facultades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_directrices_periodos_escolares_PeriodoId",
                        column: x => x.PeriodoId,
                        principalTable: "periodos_escolares",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_directrices_usuarios_CreadorId",
                        column: x => x.CreadorId,
                        principalTable: "usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "indicadores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    DescripcionIndicador = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estandar = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FrecuenciaControl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CantidadEvidencias = table.Column<int>(type: "int", nullable: false),
                    IndicadorCompletado = table.Column<bool>(type: "bit", nullable: false),
                    AccionCorrectiva = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaEmision = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaCumplimiento = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DirectrizId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GrupoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreadorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResponsableAccionCorrectivaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PeriodoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CarreraId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_indicadores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_indicadores_carreras_CarreraId",
                        column: x => x.CarreraId,
                        principalTable: "carreras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_indicadores_directrices_DirectrizId",
                        column: x => x.DirectrizId,
                        principalTable: "directrices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_indicadores_grupo_indicadores_GrupoId",
                        column: x => x.GrupoId,
                        principalTable: "grupo_indicadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_indicadores_periodos_escolares_PeriodoId",
                        column: x => x.PeriodoId,
                        principalTable: "periodos_escolares",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_indicadores_usuarios_CreadorId",
                        column: x => x.CreadorId,
                        principalTable: "usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_indicadores_usuarios_ResponsableAccionCorrectivaId",
                        column: x => x.ResponsableAccionCorrectivaId,
                        principalTable: "usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "estrategias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    DescripcionEstrategia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaEmision = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IndicadorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreadorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PeriodoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CarreraId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComentarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_estrategias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_estrategias_carreras_CarreraId",
                        column: x => x.CarreraId,
                        principalTable: "carreras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_estrategias_comentarios_ComentarioId",
                        column: x => x.ComentarioId,
                        principalTable: "comentarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_estrategias_indicadores_IndicadorId",
                        column: x => x.IndicadorId,
                        principalTable: "indicadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_estrategias_periodos_escolares_PeriodoId",
                        column: x => x.PeriodoId,
                        principalTable: "periodos_escolares",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_estrategias_usuarios_CreadorId",
                        column: x => x.CreadorId,
                        principalTable: "usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "evidencias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    NombreArchivo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contenido = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    IndicadorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_evidencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_evidencias_indicadores_IndicadorId",
                        column: x => x.IndicadorId,
                        principalTable: "indicadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "actividades",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    DescripcionActividad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CantidadLograda = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaEmision = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaCumplimiento = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ActividadCumplida = table.Column<bool>(type: "bit", nullable: false),
                    EstrategiaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreadorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PeriodoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CarreraId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComentarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_actividades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_actividades_carreras_CarreraId",
                        column: x => x.CarreraId,
                        principalTable: "carreras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_actividades_comentarios_ComentarioId",
                        column: x => x.ComentarioId,
                        principalTable: "comentarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_actividades_estrategias_EstrategiaId",
                        column: x => x.EstrategiaId,
                        principalTable: "estrategias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_actividades_periodos_escolares_PeriodoId",
                        column: x => x.PeriodoId,
                        principalTable: "periodos_escolares",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_actividades_usuarios_CreadorId",
                        column: x => x.CreadorId,
                        principalTable: "usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "facultades",
                columns: new[] { "Id", "FechaEmision", "Mision", "Nombre", "Slogan", "Vision" },
                values: new object[] { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d476"), new DateTime(2026, 3, 17, 6, 0, 0, 0, DateTimeKind.Utc), "Buscar la mejora tecnológica.", "Facultad de Tecnología", "Un mundo mejor con tecnología.", "En 2030, ser una facultad lider en avances tecnológicos." });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d473"), "Administrador" },
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d481"), "Maestro" }
                });

            migrationBuilder.InsertData(
                table: "carreras",
                columns: new[] { "Id", "FacultadId", "NombreCarrera" },
                values: new object[] { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d488"), new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d476"), "Ingeniería de Software y Sistemas Computacionales." });

            migrationBuilder.InsertData(
                table: "usuarios",
                columns: new[] { "Id", "CarreraId", "CorreoInstitucional", "FacultadId", "NombreUsuario", "RolId", "TipoUsuario" },
                values: new object[] { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d490"), new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d488"), "rcc74823@lasallebajio.edu.mx", new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d476"), "Rogelio Ceballos Castillo", new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d473"), "Administrador" });

            migrationBuilder.CreateIndex(
                name: "IX_actividades_CarreraId",
                table: "actividades",
                column: "CarreraId");

            migrationBuilder.CreateIndex(
                name: "IX_actividades_ComentarioId",
                table: "actividades",
                column: "ComentarioId");

            migrationBuilder.CreateIndex(
                name: "IX_actividades_CreadorId",
                table: "actividades",
                column: "CreadorId");

            migrationBuilder.CreateIndex(
                name: "IX_actividades_EstrategiaId",
                table: "actividades",
                column: "EstrategiaId");

            migrationBuilder.CreateIndex(
                name: "IX_actividades_PeriodoId",
                table: "actividades",
                column: "PeriodoId");

            migrationBuilder.CreateIndex(
                name: "IX_archivoPoliticas_FacultadId",
                table: "archivoPoliticas",
                column: "FacultadId");

            migrationBuilder.CreateIndex(
                name: "IX_carreras_FacultadId",
                table: "carreras",
                column: "FacultadId");

            migrationBuilder.CreateIndex(
                name: "IX_comentarios_CreadorId",
                table: "comentarios",
                column: "CreadorId");

            migrationBuilder.CreateIndex(
                name: "IX_directrices_ComentarioId",
                table: "directrices",
                column: "ComentarioId");

            migrationBuilder.CreateIndex(
                name: "IX_directrices_CreadorId",
                table: "directrices",
                column: "CreadorId");

            migrationBuilder.CreateIndex(
                name: "IX_directrices_FacultadId",
                table: "directrices",
                column: "FacultadId");

            migrationBuilder.CreateIndex(
                name: "IX_directrices_PeriodoId",
                table: "directrices",
                column: "PeriodoId");

            migrationBuilder.CreateIndex(
                name: "IX_estrategias_CarreraId",
                table: "estrategias",
                column: "CarreraId");

            migrationBuilder.CreateIndex(
                name: "IX_estrategias_ComentarioId",
                table: "estrategias",
                column: "ComentarioId");

            migrationBuilder.CreateIndex(
                name: "IX_estrategias_CreadorId",
                table: "estrategias",
                column: "CreadorId");

            migrationBuilder.CreateIndex(
                name: "IX_estrategias_IndicadorId",
                table: "estrategias",
                column: "IndicadorId");

            migrationBuilder.CreateIndex(
                name: "IX_estrategias_PeriodoId",
                table: "estrategias",
                column: "PeriodoId");

            migrationBuilder.CreateIndex(
                name: "IX_evidencias_IndicadorId",
                table: "evidencias",
                column: "IndicadorId");

            migrationBuilder.CreateIndex(
                name: "IX_indicadores_CarreraId",
                table: "indicadores",
                column: "CarreraId");

            migrationBuilder.CreateIndex(
                name: "IX_indicadores_CreadorId",
                table: "indicadores",
                column: "CreadorId");

            migrationBuilder.CreateIndex(
                name: "IX_indicadores_DirectrizId",
                table: "indicadores",
                column: "DirectrizId");

            migrationBuilder.CreateIndex(
                name: "IX_indicadores_GrupoId",
                table: "indicadores",
                column: "GrupoId");

            migrationBuilder.CreateIndex(
                name: "IX_indicadores_PeriodoId",
                table: "indicadores",
                column: "PeriodoId");

            migrationBuilder.CreateIndex(
                name: "IX_indicadores_ResponsableAccionCorrectivaId",
                table: "indicadores",
                column: "ResponsableAccionCorrectivaId");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_CarreraId",
                table: "usuarios",
                column: "CarreraId");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_FacultadId",
                table: "usuarios",
                column: "FacultadId");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_RolId",
                table: "usuarios",
                column: "RolId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "actividades");

            migrationBuilder.DropTable(
                name: "archivoPoliticas");

            migrationBuilder.DropTable(
                name: "evidencias");

            migrationBuilder.DropTable(
                name: "estrategias");

            migrationBuilder.DropTable(
                name: "indicadores");

            migrationBuilder.DropTable(
                name: "directrices");

            migrationBuilder.DropTable(
                name: "grupo_indicadores");

            migrationBuilder.DropTable(
                name: "comentarios");

            migrationBuilder.DropTable(
                name: "periodos_escolares");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "carreras");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "facultades");
        }
    }
}
