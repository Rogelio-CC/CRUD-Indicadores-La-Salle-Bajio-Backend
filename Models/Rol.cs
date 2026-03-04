using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KPIBackend.Models
{
    public class Rol : IEntity, IUniqueName
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MinLength(5, ErrorMessage = "Debe tener al menos 5 caracteres")]
        [MaxLength(100, ErrorMessage = "No puede exceder 100 caracteres")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "Los permisos son obligatorios")]
        [MinLength(5, ErrorMessage = "Debe tener al menos 5 caracteres")]
        [MaxLength(100, ErrorMessage = "No puede exceder 100 caracteres")]
        public string Permisos { get; set; } = null!;

        //[JsonIgnore]
        //public ICollection<Usuario>? Usuarios { get; set; }
    }
}
