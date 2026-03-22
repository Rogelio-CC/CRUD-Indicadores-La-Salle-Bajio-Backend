namespace KPIBackend.Application.DTOs.ListaCombos
{
    /// <summary>
    /// DTO para opciones de combo de carreras.
    /// </summary>
    public class CarreraComboDTO
    {
        /// <summary>
        /// Identificador de la carrera.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nombre de la carrera.
        /// </summary>
        public string Nombre { get; set; } = string.Empty;
    }
}
