using System.Text.Json.Serialization;

namespace KPIBackend.Models
{
    /// <summary>
    /// Representa una actividad vinculada a una estrategia, periodo escolar y usuario creador.
    /// </summary>
    public class Actividad : IEntity, IOwnedEntity
    {
        /// <summary>
        /// Identificador único de la actividad.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Descripción de la tarea o acción planificada.
        /// </summary>
        public string DescripcionActividad { get; set; } = null!;

        /// <summary>
        /// Valor numérico alcanzado por la actividad (por ejemplo, cantidad de evidencias).
        /// </summary>
        public decimal CantidadLograda { get; set; } = 0;

        /// <summary>
        /// Fecha en la que se generó o registró la actividad.
        /// </summary>
        public DateTime FechaEmision { get; set; }

        private DateTimeOffset? _fechaCumplimiento;

        /// <summary>
        /// Fecha en la que se marcó la actividad como cumplida (opcional).
        /// </summary>
        public DateTimeOffset? FechaCumplimiento
        {
            get => _fechaCumplimiento;
            set => _fechaCumplimiento = value;
        }

        /// <summary>
        /// Indica si la actividad ya fue cumplida.
        /// </summary>
        public bool ActividadCumplida { get; set; }

        /// <summary>
        /// Clave foránea a la estrategia asociada.
        /// </summary>
        public Guid EstrategiaId { get; set; }

        /// <summary>
        /// Navegación a la estrategia asociada.
        /// </summary>
        public Estrategia Estrategia { get; set; } = null!;

        /// <summary>
        /// Clave foránea al usuario que creó la actividad.
        /// </summary>
        public Guid CreadorId { get; set; }

        /// <summary>
        /// Navegación al usuario creador.
        /// </summary>
        public Usuario Creador { get; set; } = null!;

        /// <summary>
        /// Clave foránea al periodo escolar en el que se realiza la actividad.
        /// </summary>
        public Guid PeriodoId { get; set; }

        /// <summary>
        /// Navegación al periodo escolar.
        /// </summary>
        public PeriodoEscolar Periodo { get; set; } = null!;

        /// <summary>
        /// Clave foránea a la carrera vinculada.
        /// </summary>
        public Guid CarreraId { get; set; }

        /// <summary>
        /// Navegación a la carrera vinculada.
        /// </summary>
        public Carrera Carrera { get; set; } = null!;

        /// <summary>
        /// Clave foránea del comentario vinculado.
        /// </summary>
        public Guid? ComentarioId { get; set; }

        /// <summary>
        /// Navegación al comentario vinculado.
        /// </summary>
        public Comentario? Comentario { get; set; }
    }
}
