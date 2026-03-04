using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KPIBackend.Models
{
    public class PeriodoEscolar : IEntity, IUniqueName
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MinLength(5, ErrorMessage = "Debe tener al menos 5 caracteres")]
        [MaxLength(100, ErrorMessage = "No puede exceder 100 caracteres")]
        public string Nombre { get; set; } = null!;

        private DateTimeOffset _fechaInicio;
        public DateTimeOffset FechaInicio
        {
            get => _fechaInicio;
            set => _fechaInicio = value.ToUniversalTime();
        }

        private DateTimeOffset _fechaFin;
        public DateTimeOffset FechaFin
        {
            get => _fechaFin;
            set => _fechaFin = value.ToUniversalTime();
        }

        //[JsonIgnore]
        //public ICollection<Directriz>? Directrices { get; set; }

        //[JsonIgnore]
        //public ICollection<Indicador>? Indicadores { get; set; }

        //[JsonIgnore]
        //public ICollection<Estrategia>? Estrategias { get; set; }

        //[JsonIgnore]
        //public ICollection<Actividad>? Actividades { get; set; }
    }
}
