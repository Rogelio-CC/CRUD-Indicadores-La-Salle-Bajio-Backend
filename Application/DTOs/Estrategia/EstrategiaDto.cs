using KPIBackend.Models;

namespace KPIBackend.Application.DTOs.Estrategia
{
    public class EstrategiaDto
    {
        public Guid Id { get; set; }
        public string DescripcionEstrategia { get; set; } = null!;
        public DateTime FechaEmision { get; set; }

        public Guid IndicadorId { get; set; }
        public string Indicador { get; set; } = string.Empty;

        public Guid CreadorId { get; set; }
        public string Creador { get; set; } = string.Empty;

        public Guid PeriodoId { get; set; }
        public string Periodo { get; set; } = string.Empty;

        public Guid CarreraId { get; set; }
        public string Carrera { get; set; } = string.Empty;
    }
}
