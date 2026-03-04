namespace KPIBackend.Models
{
    public class EventoCalendario : IEntity
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; } = null!;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string TipoEvento { get; set; } = null!;
        public string? Color { get; set; } = null;
    }
}
