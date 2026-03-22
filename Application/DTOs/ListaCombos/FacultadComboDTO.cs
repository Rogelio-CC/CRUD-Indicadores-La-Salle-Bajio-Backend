namespace KPIBackend.Application.DTOs.ListaCombos
{
    /// <summary>
    /// DTO para opciones de combo de facultades.
    /// </summary>
    public class FacultadComboDTO
    {
        /// <summary>
        /// Identificador de la facultad.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nombre de la facultad.
        /// </summary>
        public string Nombre { get; set; } = string.Empty;
    }
}
