using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace KPIBackend.Models
{
    public class Usuario : IEntity
    {
        public Guid Id { get; set; } 
        public string NombreUsuario { get; set; } = null!;
        public string CorreoInstitucional { get; set; } = null!;
        public string TipoUsuario { get; set; } = null!;
        
        public Guid RolId { get; set; }
        public Rol Rol { get; set; } = null!;
        
        public Guid FacultadId { get; set; }
        public Facultad Facultad { get; set; } = null!;
        
        public Guid CarreraId { get; set; }
        public Carrera Carrera { get; set; } = null!;

        //[JsonIgnore]
        //public ICollection<Directriz>? DirectricesCreadas { get; set; }

        //[JsonIgnore]
        //public ICollection<Indicador>? IndicadoresCreados { get; set; }

        //[JsonIgnore]
        //public ICollection<Estrategia>? EstrategiasCreadas { get; set; }

        //[JsonIgnore]
        //public ICollection<Actividad>? ActividadesCreadas { get; set; }

        //[JsonIgnore]
        //public ICollection<Comentario>? Comentarios { get; set; }
    }
}
