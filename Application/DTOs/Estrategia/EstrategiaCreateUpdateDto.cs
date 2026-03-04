namespace KPIBackend.Application.DTOs.Estrategia
{
    public class EstrategiaCreateUpdateDto
    {
        public string DescripcionEstrategia { get; set; } = null!;
        public DateTime FechaEmision { get; set; }

        public Guid IndicadorId { get; set; }

        public Guid CreadorId { get; set; }

        public Guid PeriodoId { get; set; }

        public Guid CarreraId { get; set; }
    }
}
