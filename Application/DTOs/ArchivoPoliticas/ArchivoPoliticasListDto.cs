namespace KPIBackend.Application.DTOs.ArchivoPoliticas
{
    /// <summary>
    /// DTO para mostrar el archivo en forma de lista.
    /// </summary>
    public class ArchivoPoliticasListDto
    {
        /// <summary>
        /// Obtiene o establece el identificador único del archivo de las políticas.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Obtiene o establece el nombre del archivo de las políticas.
        /// </summary>
        public string NombreArchivo { get; set; } = null!;

        /// <summary>
        /// Obtiene o establece el tipo de la evidencia de las políticas.
        /// </summary>
        public string Tipo { get; set; } = null!;
    }
}
