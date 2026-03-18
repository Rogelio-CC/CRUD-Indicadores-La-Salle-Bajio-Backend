using KPIBackend.Models;

namespace KPIBackend.Application.DTOs.Indicador
{
    public class IndicadorDto
    {
        public Guid Id { get; set; }
        public string DescripcionIndicador { get; set; } = null!;
        public decimal Estandar { get; set; } = 0;
        public string FrecuenciaControl { get; set; } = null!;
        public int CantidadEvidencias { get; set; }
        public bool IndicadorCompletado { get; set; }
        public string? AccionCorrectiva { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTimeOffset? FechaCumplimiento { get; set; }

        public Guid DirectrizId { get; set; }
        public string Directriz { get; set; } = null!;

        public Guid GrupoId { get; set; }
        public string Grupo { get; set; } = null!;

        public Guid CreadorId { get; set; }
        public string Creador { get; set; } = null!;

        //Nuevo dato agregado por mí
        public Guid? ResponsableAccionCorrectivaId { get; set; }
        public string? ResponsableAccionCorrectiva { get; set; }

        public Guid PeriodoId { get; set; }
        public string Periodo { get; set; } = null!;

        public Guid CarreraId { get; set; }
        public string Carrera { get; set; } = null!;
    }
}
