using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace KPIBackend.Models
{
    public class Directriz : IEntity, IOwnedEntity
    {
        public Guid Id { get; set; } 
        public string Descripcion { get; set; } = null!;

        public Guid FacultadId { get; set; }
        public Facultad Facultad { get; set; } = null!;

        public Guid CreadorId { get; set; }
        public Usuario Creador { get; set; } = null!;

        public Guid PeriodoId { get; set; }
        public PeriodoEscolar Periodo { get; set; } = null!;

        //[JsonIgnore]
        //public ICollection<Indicador>? Indicadores { get; set; }

        //[JsonIgnore]
        //public ICollection<Comentario>? Comentarios { get; set; }
    }
}
