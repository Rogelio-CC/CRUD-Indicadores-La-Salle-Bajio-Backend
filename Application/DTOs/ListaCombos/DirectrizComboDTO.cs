namespace KPIBackend.Application.DTOs.ListaCombos
{
    /// <summary>
    /// DTO para opciones de combo de directrices.
    /// </summary>
    public class DirectrizComboDTO
    {
        /// <summary>
        /// Identificador de la directriz.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nombre de la directriz.
        /// </summary>
        public string Nombre { get; set; } = string.Empty;
    }
}
