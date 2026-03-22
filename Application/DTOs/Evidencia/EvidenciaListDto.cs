namespace KPIBackend.Application.DTOs.Evidencia
{
    /// <summary>
    /// DTO para listar elementos de evidencia.
    /// </summary>
    public class EvidenciaListDto
    {
        /// <summary>
        /// Obtiene o establece el identificador único de la evidencia.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Obtiene o establece el nombre del archivo.
        /// </summary>
        public string NombreArchivo { get; set; } = null!;

        /// <summary>
        /// Obtiene o establece el tipo de la evidencia.
        /// </summary>
        public string Tipo { get; set; } = null!;
    }

}
