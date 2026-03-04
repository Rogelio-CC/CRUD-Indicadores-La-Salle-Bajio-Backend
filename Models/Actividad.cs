using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace KPIBackend.Models
{
    public class Actividad : IEntity, IOwnedEntity
    {
        public Guid Id { get; set; } 
        public string DescripcionActividad { get; set; } = null!;
        public decimal CantidadLograda { get; set; } = 0;
        public DateTime FechaEmision { get; set; }

        private DateTimeOffset? _fechaCumplimiento;
        public DateTimeOffset? FechaCumplimiento
        {
            get => _fechaCumplimiento;
            set => _fechaCumplimiento = value;
        }
        public bool ActividadCumplida { get; set; }
        
        public Guid EstrategiaId { get; set; }
        public Estrategia Estrategia { get; set; } = null!;

        public Guid CreadorId { get; set; }
        public Usuario Creador { get; set; } = null!;

        public Guid PeriodoId { get; set; }
        public PeriodoEscolar Periodo { get; set; } = null!;

        public Guid CarreraId { get; set; }
        public Carrera Carrera { get; set; } = null!;

        //[JsonIgnore]
        //public ICollection<Comentario>? Comentarios { get; set; }
    }
}
