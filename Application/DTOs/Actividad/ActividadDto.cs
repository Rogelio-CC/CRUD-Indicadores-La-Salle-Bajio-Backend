namespace KPIBackend.Application.DTOs.Actividad
{
    /// <summary>
    /// DTO que representa una actividad junto con la información
    /// de sus entidades relacionadas.
    /// </summary>
    /// 
    public class ActividadDto
    {
        /// <summary>
        /// Identificador de la actividad. 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Descripción de la actividad. 
        /// </summary>
        public string DescripcionActividad { get; set; } = null!;

        /// <summary>
        ///  Valor númerico alcanzado por la actividad. 
        /// </summary>
        public decimal CantidadLograda { get; set; }

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
        /// Nombre de la estrategia asociada.
        /// </summary>
        public string Estrategia { get; set; } = null!;

        /// <summary>
        /// Identificador del usuario que creó la actividad.
        /// </summary>
        public Guid CreadorId { get; set; }

        /// <summary>
        /// Nombre del usuario creador.
        /// </summary>
        public string Creador { get; set; } = null!;

        /// <summary>
        /// Identificador del periodo escolar en el que se realiza la actividad.
        /// </summary>
        public Guid PeriodoId { get; set; }

        /// <summary>
        /// Nombre del período escolar.
        /// </summary>
        public string Periodo { get; set; } = null!;

        /// <summary>
        /// Identificador de la carrera vinculada.
        /// </summary>
        public Guid CarreraId { get; set; }

        /// <summary>
        /// Nombre de la carrera.
        /// </summary>
        public string Carrera { get; set; } = null!;
    }
}
