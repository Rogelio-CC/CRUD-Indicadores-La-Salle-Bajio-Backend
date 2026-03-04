using System;
using System.Collections.Generic;

namespace KPIBackend.Models
{
    public class Evidencia : IEntity
    {
        public Guid Id { get; set; }
        public string NombreArchivo { get; set; } = null!;
        public string Tipo { get; set; } = null!;
        public byte[] Contenido { get; set; } = Array.Empty<byte>();
        public Guid IndicadorId { get; set; }
        public Indicador Indicador { get; set; } = null!;
    }
}

