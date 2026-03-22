using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KPIBackend.Models
{
    /// <summary>
    /// Representa un período escolar utilizado para la creación de elementos como indicadores.
    /// </summary>
    public class PeriodoEscolar : IEntity, IUniqueName
    {
        /// <summary>
        /// Identificador único del período escolar.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nombre del período escolar.
        /// </summary>
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MinLength(5, ErrorMessage = "Debe tener al menos 5 caracteres")]
        [MaxLength(100, ErrorMessage = "No puede exceder 100 caracteres")]
        public string Nombre { get; set; } = null!;

        private DateTimeOffset _fechaInicio;

        /// <summary>
        /// Fecha de inicio o comienzo del período escolar.
        /// </summary>
        public DateTimeOffset FechaInicio
        {
            get => _fechaInicio;
            set => _fechaInicio = value.ToUniversalTime();
        }

        private DateTimeOffset _fechaFin;

        /// <summary>
        /// Fecha de finalización del período escolar.
        /// </summary>
        public DateTimeOffset FechaFin
        {
            get => _fechaFin;
            set => _fechaFin = value.ToUniversalTime();
        }

        /// <summary>
        /// Obtiene o establece la colección de directrices asociados al periodo escolar.
        /// </summary>
        /// <remarks>
        /// El atributo [JsonIgnore] es crítico para evitar ciclos infinitos durante la 
        /// serialización a JSON (Circular Reference) y visualización de datos sensibles.
        /// </remarks>
        [JsonIgnore]
        public ICollection<Directriz>? Directrices { get; set; }

        /// <summary>
        /// Obtiene o establece la colección de indicadores asociados al periodo escolar.
        /// </summary>
        /// <remarks>
        /// El atributo [JsonIgnore] es crítico para evitar ciclos infinitos durante la 
        /// serialización a JSON (Circular Reference) y visualización de datos sensibles.
        /// </remarks>
        [JsonIgnore]
        public ICollection<Indicador>? Indicadores { get; set; }

        /// <summary>
        /// Obtiene o establece la colección de estrategias asociados al periodo escolar.
        /// </summary>
        /// <remarks>
        /// El atributo [JsonIgnore] es crítico para evitar ciclos infinitos durante la 
        /// serialización a JSON (Circular Reference) y visualización de datos sensibles.
        /// </remarks>
        [JsonIgnore]
        public ICollection<Estrategia>? Estrategias { get; set; }

        /// <summary>
        /// Obtiene o establece la colección de actividades asociados al periodo escolar.
        /// </summary>
        /// <remarks>
        /// El atributo [JsonIgnore] es crítico para evitar ciclos infinitos durante la 
        /// serialización a JSON (Circular Reference) y visualización de datos sensibles.
        /// </remarks>
        [JsonIgnore]
        public ICollection<Actividad>? Actividades { get; set; }
    }
}
