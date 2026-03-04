namespace KPIBackend.Application.DTOs.Carrera
{
    public class CarreraCreateUpdateDto
    {
        public Guid Id { get; set; }
        public string NombreCarrera { get; set; } = null!;
        public Guid FacultadId { get; set; }
    }
}
