namespace KPIBackend.Models
{
    /// <summary>
    /// Representa un comentario asociado a una entidad del sistema
    /// (ejemplo: actividades, estrategias o directrices).
    /// </summary>
    public class Comentario : IEntity, IOwnedEntity
    {
        /// <summary>
        /// Identificador único del comentario.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Contenido textual del comentario.
        /// </summary>
        public string Contenido { get; set; } = null!;

        /// <summary>
        /// Fecha en la que se realizó el comentario.
        /// </summary>
        public DateTime FechaComentario { get; set; }

        /// <summary>
        /// Tipo de entidad a la que pertenece el comentario.
        /// </summary>
        public string TipoObjetivo { get; set; } = null!;

        /// <summary>
        /// Identificador de la entidad a la que pertenece el comentario.
        /// </summary>
        public Guid IdObjetivo { get; set; }

        /// <summary>
        /// Clave foránea del usuario que creó el comentario.
        /// </summary>
        public Guid CreadorId { get; set; }

        /// <summary>
        /// Navegación al usuario creador.
        /// </summary>
        public Usuario Creador { get; set; } = null!;
    }
}
