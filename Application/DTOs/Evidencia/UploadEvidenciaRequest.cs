using System.ComponentModel.DataAnnotations;

namespace KPIBackend.Application.DTOs.Evidencia
{
    /// <summary>
    /// Se usa como parámetro en el método "Upload" de EvidenciasController para leer correctamente el archivo tanto en la interfaz como en swagger.
    /// </summary>
    public class UploadEvidenciaRequest
    {
        /// <summary>
        /// Nombre del parámetro requerido en el método "Upload".
        /// </summary>
        [Required]
        public IFormFile File { get; set; } = null!;
    }
}
