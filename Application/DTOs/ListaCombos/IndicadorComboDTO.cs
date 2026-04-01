namespace KPIBackend.Application.DTOs.ListaCombos
{
    /// <summary>
    /// DTO para opciones de combo de indicadores.
    /// </summary>
    public class IndicadorComboDTO
    {
        /// <summary>
        /// Identificador del indicador.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nombre del indicador.
        /// </summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Nombre de la carrera del indicador.
        /// </summary>
        public string NombreCarrera { get; set; } = string.Empty;
    }
}
