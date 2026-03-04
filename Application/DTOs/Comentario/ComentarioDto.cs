using KPIBackend.Models;

namespace KPIBackend.Application.DTOs.Comentario
{
    public class ComentarioDto
    {
        public Guid Id { get; set; }
        public string Contenido { get; set; } = null!;
        public DateTime FechaComentario { get; set; }
        public string TipoObjetivo { get; set; } = null!;
        public Guid IdObjetivo { get; set; }

        public Guid CreadorId { get; set; }
        public string Creador { get; set; } = null!;
    }
}
