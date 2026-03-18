using System.ComponentModel.DataAnnotations;

namespace KPIBackend.Application.DTOs.Evidencia
{
    public class UploadEvidenciaRequest
    {
        [Required]
        public IFormFile File { get; set; } = null!;
    }
}
