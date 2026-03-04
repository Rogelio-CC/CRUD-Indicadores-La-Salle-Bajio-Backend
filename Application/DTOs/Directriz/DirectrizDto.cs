namespace KPIBackend.Application.DTOs.Directriz
{
    public class DirectrizDto
    {
        public Guid Id { get; set; }
        public string Descripcion { get; set; } = string.Empty;

        public Guid FacultadId { get; set; }
        public string Facultad { get; set; } = string.Empty;

        public Guid CreadorId { get; set; }
        public string Creador { get; set; } = string.Empty;

        public Guid PeriodoId { get; set; }
        public string Periodo { get; set; } = string.Empty;
    }
}
