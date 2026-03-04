namespace KPIBackend.Application.DTOs.Evidencia
{
    public class EvidenciaListDto
    {
        public Guid Id { get; set; }
        public string NombreArchivo { get; set; } = null!;
        public string Tipo { get; set; } = null!;
    }

}
