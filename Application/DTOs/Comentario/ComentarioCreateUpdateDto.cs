namespace KPIBackend.Application.DTOs.Comentario
{
    /// <summary>
    /// DTO para crear o actualizar comentarios.
    /// </summary>
    public class ComentarioCreateUpdateDto
    {
        /// <summary>
        /// Contenido textual del comentario.
        /// </summary>
        public string Contenido { get; set; } = null!;
        
        /// <summary>
        /// Tipo de entidad a la que pertenece el comentario.
        /// </summary>
        public string TipoObjetivo { get; set; } = null!;

        /// <summary>
        /// Identificador del usuario que creó el comentario.
        /// </summary>
        public Guid CreadorId { get; set; }
    }
}
