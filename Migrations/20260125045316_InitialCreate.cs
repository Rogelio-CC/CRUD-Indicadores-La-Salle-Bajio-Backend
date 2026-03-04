using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KPIBackend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "facultades",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    NombreFacultad = table.Column<string>(type: "text", nullable: false),
                    Mision = table.Column<string>(type: "text", nullable: false),
                    Vision = table.Column<string>(type: "text", nullable: false),
                    Slogan = table.Column<string>(type: "text", nullable: false),
                    PoliticaAsociada = table.Column<string>(type: "text", nullable: false),
                    FechaEmision = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaEdicion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
                    DescripcionGrupo = table.Column<string>(type: "text", nullable: false)
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
                    NombrePeriodo = table.Column<string>(type: "text", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
                    NombreRol = table.Column<string>(type: "text", nullable: false),
                    Permisos = table.Column<string>(type: "text", nullable: false)
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
                    IdFacultad = table.Column<Guid>(type: "uuid", nullable: false),
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
                    IdRol = table.Column<Guid>(type: "uuid", nullable: false),
                    RolId = table.Column<Guid>(type: "uuid", nullable: false),
                    IdFacultad = table.Column<Guid>(type: "uuid", nullable: false),
                    FacultadId = table.Column<Guid>(type: "uuid", nullable: false),
                    IdCarrera = table.Column<Guid>(type: "uuid", nullable: false),
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
                name: "comentarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Contenido = table.Column<string>(type: "text", nullable: false),
                    FechaComentario = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TipoObjetivo = table.Column<string>(type: "text", nullable: false),
                    IdObjetivo = table.Column<Guid>(type: "uuid", nullable: false),
                    IdUsuario = table.Column<Guid>(type: "uuid", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comentarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_comentarios_usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "directrices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    IdFacultad = table.Column<Guid>(type: "uuid", nullable: false),
                    FacultadId = table.Column<Guid>(type: "uuid", nullable: false),
                    IdCreador = table.Column<Guid>(type: "uuid", nullable: false),
                    CreadorId = table.Column<Guid>(type: "uuid", nullable: false),
                    IdPeriodo = table.Column<Guid>(type: "uuid", nullable: false),
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
                    IndicadorCompletado = table.Column<string>(type: "text", nullable: false),
                    AccionCorrectiva = table.Column<string>(type: "text", nullable: false),
                    FechaEmision = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaCumplimiento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IdDirectriz = table.Column<Guid>(type: "uuid", nullable: false),
                    DirectrizId = table.Column<Guid>(type: "uuid", nullable: false),
                    IdGrupo = table.Column<Guid>(type: "uuid", nullable: false),
                    GrupoId = table.Column<Guid>(type: "uuid", nullable: false),
                    IdCreador = table.Column<Guid>(type: "uuid", nullable: false),
                    CreadorId = table.Column<Guid>(type: "uuid", nullable: false),
                    IdResponsableAccionCorrectiva = table.Column<Guid>(type: "uuid", nullable: false),
                    ResponsableAccionCorrectivaId = table.Column<Guid>(type: "uuid", nullable: false),
                    IdPeriodo = table.Column<Guid>(type: "uuid", nullable: false),
                    PeriodoId = table.Column<Guid>(type: "uuid", nullable: false),
                    IdCarrera = table.Column<Guid>(type: "uuid", nullable: false),
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
                    IdIndicador = table.Column<Guid>(type: "uuid", nullable: false),
                    IndicadorId = table.Column<Guid>(type: "uuid", nullable: false),
                    IdCreador = table.Column<Guid>(type: "uuid", nullable: false),
                    CreadorId = table.Column<Guid>(type: "uuid", nullable: false),
                    IdPeriodo = table.Column<Guid>(type: "uuid", nullable: false),
                    PeriodoId = table.Column<Guid>(type: "uuid", nullable: false),
                    IdCarrera = table.Column<Guid>(type: "uuid", nullable: false),
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
                    FechaCumplimiento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Cumplida = table.Column<bool>(type: "boolean", nullable: false),
                    IdEstrategia = table.Column<Guid>(type: "uuid", nullable: false),
                    EstrategiaId = table.Column<Guid>(type: "uuid", nullable: false),
                    IdCreador = table.Column<Guid>(type: "uuid", nullable: false),
                    CreadorId = table.Column<Guid>(type: "uuid", nullable: false),
                    IdPeriodo = table.Column<Guid>(type: "uuid", nullable: false),
                    PeriodoId = table.Column<Guid>(type: "uuid", nullable: false),
                    IdCarrera = table.Column<Guid>(type: "uuid", nullable: false),
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
                name: "IX_comentarios_UsuarioId",
                table: "comentarios",
                column: "UsuarioId");

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
                name: "actividades");

            migrationBuilder.DropTable(
                name: "comentarios");

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
