namespace KPIBackend.Application.DTOs.ListaCombos
{
    /// <summary>
    /// DTO para opciones de combo de períodos escolares.
    /// </summary>
    public class PeriodoEscolarComboDTO
    {
        /// <summary>
        /// Identificador del período escolar.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nombre del período escolar.
        /// </summary>
        public string Nombre { get; set; } = string.Empty;
    }
}
