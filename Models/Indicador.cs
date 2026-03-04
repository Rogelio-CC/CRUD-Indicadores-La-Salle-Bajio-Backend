using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace KPIBackend.Models
{
    public class Indicador : IEntity, IOwnedEntity
    {
        public Guid Id { get; set; }
        public string DescripcionIndicador { get; set; } = null!;
        public decimal Estandar { get; set; } = 0;
        public string FrecuenciaControl { get; set; } = null!;
        public int CantidadEvidencias { get; set; }
        public bool IndicadorCompletado { get; set; }
        public string? AccionCorrectiva { get; set; }
        public DateTime FechaEmision { get; set; }

        private DateTimeOffset? _fechaCumplimiento;
        public DateTimeOffset? FechaCumplimiento
        {
            get => _fechaCumplimiento;
            set => _fechaCumplimiento = value;
        }

        public Guid DirectrizId { get; set; }
        public Directriz Directriz { get; set; } = null!;
        
        public Guid GrupoId { get; set; }
        public GrupoIndicadores Grupo { get; set; } = null!;
        
        public Guid CreadorId { get; set; }
        public Usuario Creador { get; set; } = null!;

        //Nuevo dato agregado por mí
        public Guid? ResponsableAccionCorrectivaId { get; set; }
        public Usuario? ResponsableAccionCorrectiva { get; set; }

        public Guid PeriodoId { get; set; }
        public PeriodoEscolar Periodo { get; set; } = null!;
        
        public Guid CarreraId { get; set; }
        public Carrera Carrera { get; set; } = null!;

        public ICollection<Evidencia> Evidencias { get; set; } = new List<Evidencia>();

        //[JsonIgnore]
        //public ICollection<Estrategia>? Estrategias { get; set; }

        //[JsonIgnore]
        //public ICollection<Comentario>? Comentarios { get; set; }
    }
}
