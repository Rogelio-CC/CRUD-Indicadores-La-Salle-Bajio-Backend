using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KPIBackend.Models
{
    public class Facultad : IEntity, IUniqueName
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MinLength(5, ErrorMessage = "Debe tener al menos 5 caracteres")]
        [MaxLength(100, ErrorMessage = "No puede exceder 100 caracteres")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "La misión es obligatorio")]
        [MinLength(5, ErrorMessage = "Debe tener al menos 5 caracteres")]
        [MaxLength(100, ErrorMessage = "No puede exceder 100 caracteres")]
        public string Mision { get; set; } = null!;

        [Required(ErrorMessage = "La visión es obligatorio")]
        [MinLength(5, ErrorMessage = "Debe tener al menos 5 caracteres")]
        [MaxLength(100, ErrorMessage = "No puede exceder 100 caracteres")]
        public string Vision { get; set; } = null!;

        [Required(ErrorMessage = "El slogan es obligatorio")]
        [MinLength(5, ErrorMessage = "Debe tener al menos 5 caracteres")]
        [MaxLength(100, ErrorMessage = "No puede exceder 100 caracteres")]
        public string Slogan { get; set; } = null!;

        [Required(ErrorMessage = "La política asociada es obligatorio")]
        [MinLength(5, ErrorMessage = "Debe tener al menos 5 caracteres")]
        [MaxLength(100, ErrorMessage = "No puede exceder 100 caracteres")]
        public string PoliticaAsociada { get; set; } = null!;
        public DateTime FechaEmision { get; set; }
        public DateTime? FechaEdicion { get; set; }

        [JsonIgnore]
        public ICollection<Usuario>? Usuarios { get; set; }

        //[JsonIgnore]
        //public ICollection<Directriz>? Directrices { get; set; }

        //[JsonIgnore]
        //public ICollection<Carrera>? Carreras { get; set; }
    }
}
