namespace KPIBackend.Application.DTOs.Actividad
{
    /// <summary>
    /// DTO utilizado para crear o actualizar una actividad.
    /// </summary>
    /// 
    public class ActividadCreateUpdateDto
    {
        /// <summary>
        /// Descripción de la actividad. 
        /// </summary>
        public string DescripcionActividad { get; set; } = null!;

        /// <summary>
        ///  Valor númerico alcanzado por la actividad. 
        /// </summary>
        public decimal CantidadLograda { get; set; } = 0;

        /// <summary>
        ///  Fecha en la que se emite la actividad. 
        /// </summary>
        public DateTime FechaEmision { get; set; }

         /// <summary>
        /// Fecha en que se completó la actividad.
        /// </summary>
        public DateTimeOffset? FechaCumplimiento { get; set; }

        /// <summary>
        /// Indica si la actividad ya fue cumplida.
        /// </summary>
        public bool ActividadCumplida { get; set; }

        /// <summary>
        /// Identificador de la estrategia asociada. 
        /// </summary>
        public Guid EstrategiaId { get; set; }

        /// <summary>
        /// Identificador del usuario que creó la actividad.
        /// </summary>
        public Guid CreadorId { get; set; }

        /// <summary>
        /// Identificador del periodo escolar en el que se realiza la actividad.
        /// </summary>
        public Guid PeriodoId { get; set; }

        /// <summary>
        /// Identificador de la carrera vinculada.
        /// </summary>
        public Guid CarreraId { get; set; }
    }
}
