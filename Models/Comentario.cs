using System;
using System.Text.Json.Serialization;

namespace KPIBackend.Models
{
    public class Comentario : IEntity, IOwnedEntity
    {
        public Guid Id { get; set; } 
        public string Contenido { get; set; } = null!;
        public DateTime FechaComentario { get; set; }
        public string TipoObjetivo { get; set; } = null!;
        public Guid IdObjetivo { get; set; }
        
        public Guid CreadorId { get; set; }
        public Usuario Creador { get; set; } = null!;
    }
}
