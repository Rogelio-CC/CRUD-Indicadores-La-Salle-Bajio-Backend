namespace KPIBackend.Application.DTOs.Comentario
{
    /// <summary>
    /// DTO que representa un comentario del sistema.
    /// </summary>
    public class ComentarioDto
    {
        /// <summary>
        /// Identificador del comentario.
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
        /// Identificador del usuario que creó el comentario.
        /// </summary>
        public Guid CreadorId { get; set; }

        /// <summary>
        /// Nombre del usuario.
        /// </summary>
        public string Creador { get; set; } = null!;
    }
}
