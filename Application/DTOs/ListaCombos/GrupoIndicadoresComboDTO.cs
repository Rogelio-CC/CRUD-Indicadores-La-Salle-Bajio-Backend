namespace KPIBackend.Application.DTOs.ListaCombos
{
    /// <summary>
    /// DTO para opciones de combo de grupos de indicadores.
    /// </summary>
    public class GrupoIndicadoresComboDTO
    {
        /// <summary>
        /// Identificador del grupo de indicadores.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nombre del grupo de indicadores.
        /// </summary>
        public string Nombre { get; set; } = string.Empty;
    }
}
