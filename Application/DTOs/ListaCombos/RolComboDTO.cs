namespace KPIBackend.Application.DTOs.ListaCombos
{
    /// <summary>
    /// DTO para opciones de combo de roles.
    /// </summary>
    public class RolComboDTO
    {
        /// <summary>
        /// Identificador del rol.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nombre del rol.
        /// </summary>
        public string Nombre { get; set; } = string.Empty;
    }
}
