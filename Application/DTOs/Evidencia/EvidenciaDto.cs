namespace KPIBackend.Application.DTOs.Evidencia
{
    /// <summary>
    /// DTO que representa una evidencia asociada a un indicador.
    /// </summary>
    public class EvidenciaDto
    {
        /// <summary>
        /// Identificador de la evidencia.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nombre del archivo de la evidencia.
        /// </summary>
        public string NombreArchivo { get; set; } = null!;

        /// <summary>
        /// Tipo o extensión del archivo.
        /// </summary>
        public string Tipo { get; set; } = null!;

        /// <summary>
        /// Contenido binario del archivo.
        /// </summary>
        public byte[] Contenido { get; set; } = Array.Empty<byte>();

        /// <summary>
        /// Identificador del indicador al que pertenece la evidencia.
        /// </summary>
        public Guid IndicadorId { get; set; }

        /// <summary>
        /// Nombre del indicador.
        /// </summary>
        public string Indicador { get; set; } = null!;
    }
}
