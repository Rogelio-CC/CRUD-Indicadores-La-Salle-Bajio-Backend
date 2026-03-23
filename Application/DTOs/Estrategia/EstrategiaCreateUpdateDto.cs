namespace KPIBackend.Application.DTOs.Estrategia
{
    /// <summary>
    /// DTO para crear o actualizar estrategias.
    /// </summary>
    public class EstrategiaCreateUpdateDto
    {
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
        /// Identificador del usuario que creó la estrategia.
        /// </summary>
        public Guid CreadorId { get; set; }

        /// <summary>
        /// Identificador del período escolar en el que aplica la estrategia.
        /// </summary>
        public Guid PeriodoId { get; set; }

        /// <summary>
        /// Identificador de la carrera al que aplica la estrategia.
        /// </summary>
        public Guid CarreraId { get; set; }

        /// <summary>
        /// Identificador del del comentario vinculado.
        /// </summary>
        public Guid? ComentarioId { get; set; }
    }
}
