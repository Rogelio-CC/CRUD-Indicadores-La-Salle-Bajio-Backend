namespace KPIBackend.Models
{
    /// <summary>
    /// Representa un archivo con las políticas de la facultad.
    /// </summary>
    public class ArchivoPoliticas: IEntity
    {
        /// <summary>
        /// Identificador único del archivo de las políticas.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nombre del archivo de las políticas.
        /// </summary>
        public string NombreArchivo { get; set; } = null!;

        /// <summary>
        /// Tipo o extensión del archivo de las políticas.
        /// </summary>
        public string Tipo { get; set; } = null!;

        /// <summary>
        /// Contenido binario del archivo de las políticas.
        /// </summary>
        public byte[] Contenido { get; set; } = Array.Empty<byte>();

        /// <summary>
        /// Clave foránea de la facultad al que pertenece el archivo de políticas.
        /// </summary>
        public Guid FacultadId { get; set; }

        /// <summary>
        /// Navegación de la facultad.
        /// </summary>
        public Facultad Facultad { get; set; } = null!;
    }
}
