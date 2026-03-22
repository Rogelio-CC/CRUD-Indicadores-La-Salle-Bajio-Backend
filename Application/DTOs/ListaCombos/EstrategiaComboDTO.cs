namespace KPIBackend.Application.DTOs.ListaCombos
{
    // <summary>
    /// DTO para opciones de combo de estrategias.
    /// </summary>
    public class EstrategiaComboDTO
    {
        /// <summary>
        /// Identificador de la estrategia.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nombre de la estrategia.
        /// </summary>
        public string Nombre { get; set; } = string.Empty;
    }
}
