namespace KPIBackend.Application.DTOs.ListaCombos
{
    /// <summary>
    /// DTO para opciones de combo de comenatrios por medio del Id del objetivo.
    /// </summary>
    public class ComentarioComboDTO
    {
        /// <summary>
        /// Identificador del objetivo del comentario.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nombre o descripción del comentario vinculado.
        /// </summary>
        public string Nombre { get; set; } = string.Empty;
    }
}
