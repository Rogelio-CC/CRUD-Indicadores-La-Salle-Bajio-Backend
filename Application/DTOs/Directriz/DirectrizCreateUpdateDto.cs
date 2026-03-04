namespace KPIBackend.Application.DTOs.Directriz
{
    public class DirectrizCreateUpdateDto
    {
        public string Descripcion { get; set; } = null!;
        public Guid FacultadId { get; set; }
        public Guid CreadorId { get; set; }
        public Guid PeriodoId { get; set; }
    }


}
