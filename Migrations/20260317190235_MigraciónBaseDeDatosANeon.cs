using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

// Esta migración sirvió para crear todas las tablas, relaciones entre entidades y creación de datos iniciales y referentes a Rol, Facultad, Carrera, Usuario y EventoCalendario.

namespace KPIBackend.Migrations
{
    /// <inheritdoc />
    public partial class MigraciónBaseDeDatosANeon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "eventosCalendario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Titulo = table.Column<string>(type: "text", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TipoEvento = table.Column<string>(type: "text", nullable: false),
                    Color = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eventosCalendario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "facultades",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Mision = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Vision = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Slogan = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PoliticaAsociada = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    FechaEmision = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaEdicion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_facultades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "grupo_indicadores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    numeroGrupo = table.Column<int>(type: "integer", nullable: false),
                    DescripcionGrupo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_grupo_indicadores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "periodos_escolares",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    FechaInicio = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    FechaFin = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_periodos_escolares", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Permisos = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "carreras",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    NombreCarrera = table.Column<string>(type: "text", nullable: false),
                    FacultadId = table.Column<Guid>(type: "uuid", nullable: false)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    NombreUsuario = table.Column<string>(type: "text", nullable: false),
                    CorreoInstitucional = table.Column<string>(type: "text", nullable: false),
                    TipoUsuario = table.Column<string>(type: "text", nullable: false),
                    RolId = table.Column<Guid>(type: "uuid", nullable: false),
                    FacultadId = table.Column<Guid>(type: "uuid", nullable: false),
                    CarreraId = table.Column<Guid>(type: "uuid", nullable: false)
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
                name: "directrices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    FacultadId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreadorId = table.Column<Guid>(type: "uuid", nullable: false),
                    PeriodoId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_directrices", x => x.Id);
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    DescripcionIndicador = table.Column<string>(type: "text", nullable: false),
                    Estandar = table.Column<decimal>(type: "numeric", nullable: false),
                    FrecuenciaControl = table.Column<string>(type: "text", nullable: false),
                    CantidadEvidencias = table.Column<int>(type: "integer", nullable: false),
                    IndicadorCompletado = table.Column<bool>(type: "boolean", nullable: false),
                    AccionCorrectiva = table.Column<string>(type: "text", nullable: true),
                    FechaEmision = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaCumplimiento = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    DirectrizId = table.Column<Guid>(type: "uuid", nullable: false),
                    GrupoId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreadorId = table.Column<Guid>(type: "uuid", nullable: false),
                    ResponsableAccionCorrectivaId = table.Column<Guid>(type: "uuid", nullable: true),
                    PeriodoId = table.Column<Guid>(type: "uuid", nullable: false),
                    CarreraId = table.Column<Guid>(type: "uuid", nullable: false)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    DescripcionEstrategia = table.Column<string>(type: "text", nullable: false),
                    FechaEmision = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IndicadorId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreadorId = table.Column<Guid>(type: "uuid", nullable: false),
                    PeriodoId = table.Column<Guid>(type: "uuid", nullable: false),
                    CarreraId = table.Column<Guid>(type: "uuid", nullable: false)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    NombreArchivo = table.Column<string>(type: "text", nullable: false),
                    Tipo = table.Column<string>(type: "text", nullable: false),
                    Contenido = table.Column<byte[]>(type: "bytea", nullable: false),
                    IndicadorId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_evidencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_evidencias_indicadores_IndicadorId",
                        column: x => x.IndicadorId,
                        principalTable: "indicadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "actividades",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    DescripcionActividad = table.Column<string>(type: "text", nullable: false),
                    CantidadLograda = table.Column<decimal>(type: "numeric", nullable: false),
                    FechaEmision = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaCumplimiento = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ActividadCumplida = table.Column<bool>(type: "boolean", nullable: false),
                    EstrategiaId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreadorId = table.Column<Guid>(type: "uuid", nullable: false),
                    PeriodoId = table.Column<Guid>(type: "uuid", nullable: false),
                    CarreraId = table.Column<Guid>(type: "uuid", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "comentarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Contenido = table.Column<string>(type: "text", nullable: false),
                    FechaComentario = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TipoObjetivo = table.Column<string>(type: "text", nullable: false),
                    IdObjetivo = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    CreadorId = table.Column<Guid>(type: "uuid", nullable: false),
                    ActividadId = table.Column<Guid>(type: "uuid", nullable: true),
                    DirectrizId = table.Column<Guid>(type: "uuid", nullable: true),
                    EstrategiaId = table.Column<Guid>(type: "uuid", nullable: true),
                    IndicadorId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comentarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_comentarios_actividades_ActividadId",
                        column: x => x.ActividadId,
                        principalTable: "actividades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_comentarios_directrices_DirectrizId",
                        column: x => x.DirectrizId,
                        principalTable: "directrices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_comentarios_estrategias_EstrategiaId",
                        column: x => x.EstrategiaId,
                        principalTable: "estrategias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_comentarios_indicadores_IndicadorId",
                        column: x => x.IndicadorId,
                        principalTable: "indicadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_comentarios_usuarios_CreadorId",
                        column: x => x.CreadorId,
                        principalTable: "usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "eventosCalendario",
                columns: new[] { "Id", "Color", "FechaFin", "FechaInicio", "TipoEvento", "Titulo" },
                values: new object[,]
                {
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d475"), "#BFBFBF", new DateTime(2026, 6, 27, 6, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 2, 9, 6, 0, 0, 0, DateTimeKind.Utc), "Académico", "Inicio del semestre escolar" },
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d480"), null, new DateTime(2026, 3, 27, 6, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 20, 6, 0, 0, 0, DateTimeKind.Utc), "Académico", "Semana sin actividad" }
                });

            migrationBuilder.InsertData(
                table: "facultades",
                columns: new[] { "Id", "FechaEdicion", "FechaEmision", "Mision", "Nombre", "PoliticaAsociada", "Slogan", "Vision" },
                values: new object[] { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d476"), null, new DateTime(2026, 3, 17, 6, 0, 0, 0, DateTimeKind.Utc), "Buscar la mejora tecnológica.", "Facultad de Tecnología", "Política 1: uso ético de la tecnología.", "Un mundo mejor con tecnología.", "En 2030, ser una facultad lider en avances tecnológicos." });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "Id", "Nombre", "Permisos" },
                values: new object[,]
                {
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d473"), "Administrador", "Puede modificar todas las tablas." },
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d481"), "Maestro", "Puede crear actividades." }
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
                name: "IX_carreras_FacultadId",
                table: "carreras",
                column: "FacultadId");

            migrationBuilder.CreateIndex(
                name: "IX_comentarios_ActividadId",
                table: "comentarios",
                column: "ActividadId");

            migrationBuilder.CreateIndex(
                name: "IX_comentarios_CreadorId",
                table: "comentarios",
                column: "CreadorId");

            migrationBuilder.CreateIndex(
                name: "IX_comentarios_DirectrizId",
                table: "comentarios",
                column: "DirectrizId");

            migrationBuilder.CreateIndex(
                name: "IX_comentarios_EstrategiaId",
                table: "comentarios",
                column: "EstrategiaId");

            migrationBuilder.CreateIndex(
                name: "IX_comentarios_IndicadorId",
                table: "comentarios",
                column: "IndicadorId");

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
                name: "comentarios");

            migrationBuilder.DropTable(
                name: "eventosCalendario");

            migrationBuilder.DropTable(
                name: "evidencias");

            migrationBuilder.DropTable(
                name: "actividades");

            migrationBuilder.DropTable(
                name: "estrategias");

            migrationBuilder.DropTable(
                name: "indicadores");

            migrationBuilder.DropTable(
                name: "directrices");

            migrationBuilder.DropTable(
                name: "grupo_indicadores");

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
