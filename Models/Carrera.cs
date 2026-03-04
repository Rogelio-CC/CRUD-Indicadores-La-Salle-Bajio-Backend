using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace KPIBackend.Models
{
    public class Carrera : IEntity
    {
        public Guid Id { get; set; } 
        public string NombreCarrera { get; set; } = null!;
        
        public Guid FacultadId { get; set; }
        public Facultad Facultad { get; set; } = null!;

        //[JsonIgnore]
        //public ICollection<Usuario>? Usuarios { get; set; }

        //[JsonIgnore]
        //public ICollection<Indicador>? Indicadores { get; set; }

        //[JsonIgnore]
        //public ICollection<Estrategia>? Estrategias { get; set; }

        //[JsonIgnore]
        //public ICollection<Actividad>? Actividades { get; set; }
    }
}
