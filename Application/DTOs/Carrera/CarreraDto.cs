using KPIBackend.Models;

namespace KPIBackend.Application.DTOs.Carrera
{
    public class CarreraDto
    {
        public Guid Id { get; set; }
        public string NombreCarrera { get; set; } = null!;

        public Guid FacultadId { get; set; }
        public string Facultad { get; set; } = string.Empty;
    }
}
