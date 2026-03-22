namespace KPIBackend.Models
{
    /// <summary>
    /// Representa una evidencia asociada a un indicador.
    /// </summary>
    public class Evidencia : IEntity
    {
        /// <summary>
        /// Identificador único de la evidencia.
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
        /// Clave foránea del indicador al que pertenece la evidencia.
        /// </summary>
        public Guid IndicadorId { get; set; }

        /// <summary>
        /// Navegación del indicador.
        /// </summary>
        public Indicador Indicador { get; set; } = null!;
    }
}

