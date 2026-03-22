namespace KPIBackend.Application.DTOs.Estrategia
{
    /// <summary>
    /// DTO para representar una estrategia y sus relaciones.
    /// </summary>
    public class EstrategiaDto
    {
        /// <summary>
        /// Identificador de la estrategia.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Descripción de la estrategia.
        /// </summary>
        public string DescripcionEstrategia { get; set; } = null!;

        /// <summary>
        /// Fecha en que se emitió la estrategia.
        /// </summary>
        public DateTime FechaEmision { get; set; }

        /// <summary>
        /// Identificador del indicador al que aplica la estrategia.
        /// </summary>
        public Guid IndicadorId { get; set; }

        /// <summary>
        /// Nombre del indicador.
        /// </summary>
        public string Indicador { get; set; } = string.Empty;

        /// <summary>
        /// Identificador del usuario que creó la estrategia.
        /// </summary>
        public Guid CreadorId { get; set; }

        /// <summary>
        /// Nombre del usuario.
        /// </summary>
        public string Creador { get; set; } = string.Empty;

        /// <summary>
        /// Identificador del período escolar en el que aplica la estrategia.
        /// </summary>
        public Guid PeriodoId { get; set; }

        /// <summary>
        /// Nombre del período escolar.
        /// </summary>
        public string Periodo { get; set; } = string.Empty;

        /// <summary>
        /// Identificador de la carrera al que aplica la estrategia.
        /// </summary>
        public Guid CarreraId { get; set; }

        /// <summary>
        /// Nombre de la carrera.
        /// </summary>
        public string Carrera { get; set; } = string.Empty;
    }
}
