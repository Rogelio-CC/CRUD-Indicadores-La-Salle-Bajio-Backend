using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace KPIBackend.Models
{
    public class Estrategia : IEntity, IOwnedEntity
    {
        public Guid Id { get; set; } 
        public string DescripcionEstrategia { get; set; } = null!;
        public DateTime FechaEmision { get; set; }
        
        public Guid IndicadorId { get; set; }
        public Indicador Indicador { get; set; } = null!;
        
        public Guid CreadorId { get; set; }
        public Usuario Creador { get; set; } = null!;
        
        public Guid PeriodoId { get; set; }
        public PeriodoEscolar Periodo { get; set; } = null!;
        
        public Guid CarreraId { get; set; }
        public Carrera Carrera { get; set; } = null!;

        //[JsonIgnore]
        //public ICollection<Actividad>? Actividades { get; set; }

        //[JsonIgnore]
        //public ICollection<Comentario>? Comentarios { get; set; }
    }
}
