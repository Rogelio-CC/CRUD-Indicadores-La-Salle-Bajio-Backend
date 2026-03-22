namespace KPIBackend.Application.DTOs.ListaCombos
{
    /// <summary>
    /// DTO para opciones de combo de usuarios.
    /// </summary>
    public class UsuarioComboDTO
    {
        /// <summary>
        /// Identificador del usuario.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nombre del usuario.
        /// </summary>
        public string Nombre { get; set; } = string.Empty;
    }
}
