namespace KPIBackend.Application.DTOs.Indicador
{
    /// <summary>
    /// DTO para representar un indicador.
    /// </summary>
    public class IndicadorDto
    {
        /// <summary>
        /// Identificador del indicador.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Descripción del indicador.
        /// </summary>
        public string DescripcionIndicador { get; set; } = null!;

        /// <summary>
        /// Estándar (porcentaje) del indicador.
        /// </summary>
        public decimal Estandar { get; set; } = 0;

        /// <summary>
        /// Frecuencia de control (cada cuando se administra el indicador: mensual, semanal, diario, anual).
        /// </summary>
        public string FrecuenciaControl { get; set; } = null!;

        /// <summary>
        /// Cantidad de evidencias requeridas.
        /// </summary>
        public int CantidadEvidencias { get; set; }

        /// <summary>
        /// Indica si el indicador está completado.
        /// </summary>
        public bool IndicadorCompletado { get; set; }

        /// <summary>
        /// Acción correctiva opcional.
        /// </summary>
        public string? AccionCorrectiva { get; set; }

        /// <summary>
        /// Fecha en la que se emite el indicador.
        /// </summary>
        public DateTime FechaEmision { get; set; }

        /// <summary>
        /// Fecha en la que se cumplió el indicador.
        /// </summary>
        public DateTimeOffset? FechaCumplimiento { get; set; }

        /// <summary>
        /// Identificador de la directriz a la que aplica el indicador.
        /// </summary>
        public Guid DirectrizId { get; set; }

        /// <summary>
        /// Nombre de la directriz.
        /// </summary>
        public string Directriz { get; set; } = null!;

        /// <summary>
        /// Identificador del grupo a la que aplica el indicador.
        /// </summary>
        public Guid GrupoId { get; set; }

        /// <summary>
        /// Nombre del grupo.
        /// </summary>
        public string Grupo { get; set; } = null!;

        /// <summary>
        /// Identificador del usuario que creó el indicador.
        /// </summary>
        public Guid CreadorId { get; set; }

        /// <summary>
        /// Nombre del usuario.
        /// </summary>
        public string Creador { get; set; } = null!;

        /// <summary>
        /// Identificador opcional del usuario responsable de la acción correctiva.
        /// </summary>
        public Guid? ResponsableAccionCorrectivaId { get; set; }

        /// <summary>
        /// Nombre del responsable (usuario).
        /// </summary>
        public string? ResponsableAccionCorrectiva { get; set; }

        /// <summary>
        /// Identificador del período escolar a la que aplica el indicador.
        /// </summary>
        public Guid PeriodoId { get; set; }

        /// <summary>
        /// Nombre del período escolar.
        /// </summary>
        public string Periodo { get; set; } = null!;

        /// <summary>
        /// Identificador de la carrera a la que pertenece el indicador.
        /// </summary>
        public Guid CarreraId { get; set; }

        /// <summary>
        /// Nombre de la carrera.
        /// </summary>
        public string Carrera { get; set; } = null!;
    }
}
