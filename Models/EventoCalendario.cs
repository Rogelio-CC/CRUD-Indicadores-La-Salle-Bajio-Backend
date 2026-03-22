namespace KPIBackend.Models
{
    /// <summary>
    /// Representa un evento mostrado en el calendario del sistema.
    /// </summary>
    public class EventoCalendario : IEntity
    {
        /// <summary>
        /// Identificador único del evento.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Título del evento.
        /// </summary>
        public string Titulo { get; set; } = null!;

        /// <summary>
        /// Fecha de inicio o comienzo del evento.
        /// </summary>
        public DateTime FechaInicio { get; set; }

        /// <summary>
        /// Fecha de finalización del evento.
        /// </summary>
        public DateTime FechaFin { get; set; }

        /// <summary>
        /// Tipo de evento (por ejemplo: entrega, revisión, cierre).
        /// </summary>
        public string TipoEvento { get; set; } = null!;

        /// <summary>
        /// Color opcional para la identificación del evento.
        /// </summary>
        public string? Color { get; set; } = null;
    }
}
