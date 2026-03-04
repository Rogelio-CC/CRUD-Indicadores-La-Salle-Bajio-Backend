using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KPIBackend.Models
{
    public class GrupoIndicadores : IEntity, IUniqueNumber
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "El número del grupo es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "El valor no puede ser negativo.")]
        public int numeroGrupo { get; set; }

        [Required(ErrorMessage = "La descripción es obligatorio")]
        [MinLength(5, ErrorMessage = "Debe tener al menos 5 caracteres")]
        [MaxLength(100, ErrorMessage = "No puede exceder 100 caracteres")]
        public string DescripcionGrupo { get; set; } = null!;

        //[JsonIgnore]
        //public ICollection<Indicador>? Indicadores { get; set; }
    }
}
