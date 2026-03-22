using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KPIBackend.Models
{
    /// <summary>
    /// Representa una facultad dentro de la institución.
    /// Contiene información institucional como misión, visión y política asociada.
    /// </summary>
    public class Facultad : IEntity, IUniqueName
    {
        /// <summary>
        /// Identificador único de la facultad.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nombre de la facultad.
        /// </summary>
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MinLength(5, ErrorMessage = "Debe tener al menos 5 caracteres")]
        [MaxLength(100, ErrorMessage = "No puede exceder 100 caracteres")]
        public string Nombre { get; set; } = null!;

        /// <summary>
        /// Misión (próposito) de la facultad.
        /// </summary>
        [Required(ErrorMessage = "La misión es obligatorio")]
        [MinLength(5, ErrorMessage = "Debe tener al menos 5 caracteres")]
        [MaxLength(100, ErrorMessage = "No puede exceder 100 caracteres")]
        public string Mision { get; set; } = null!;

        /// <summary>
        /// Visión (definición de objetivos a largo plazo) de la facultad.
        /// </summary>
        [Required(ErrorMessage = "La visión es obligatorio")]
        [MinLength(5, ErrorMessage = "Debe tener al menos 5 caracteres")]
        [MaxLength(100, ErrorMessage = "No puede exceder 100 caracteres")]
        public string Vision { get; set; } = null!;

        /// <summary>
        /// Frase o dicho muy usado en la facultad.
        /// </summary>
        [Required(ErrorMessage = "El slogan es obligatorio")]
        [MinLength(5, ErrorMessage = "Debe tener al menos 5 caracteres")]
        [MaxLength(100, ErrorMessage = "No puede exceder 100 caracteres")]
        public string Slogan { get; set; } = null!;

        /// <summary>
        /// Política (norma o principio) asociada a la facultad.
        /// </summary>
        [Required(ErrorMessage = "La política asociada es obligatorio")]
        [MinLength(5, ErrorMessage = "Debe tener al menos 5 caracteres")]
        [MaxLength(100, ErrorMessage = "No puede exceder 100 caracteres")]
        public string PoliticaAsociada { get; set; } = null!;

        /// <summary>
        /// Fecha en la que se emite la facultad.
        /// </summary>
        public DateTime FechaEmision { get; set; }

        /// <summary>
        /// Fecha en la que se editó la facultad.
        /// </summary>
        public DateTime? FechaEdicion { get; set; }

        /// <summary>
        /// Obtiene o establece la colección de usuarios asociados a la facultad.
        /// </summary>
        /// <remarks>
        /// El atributo [JsonIgnore] es crítico para evitar ciclos infinitos durante la 
        /// serialización a JSON (Circular Reference) y visualización de datos sensibles.
        /// </remarks>
        [JsonIgnore]
        public ICollection<Usuario>? Usuarios { get; set; }

        /// <summary>
        /// Obtiene o establece la colección de directrices asociados a la facultad.
        /// </summary>
        /// <remarks>
        /// El atributo [JsonIgnore] es crítico para evitar ciclos infinitos durante la 
        /// serialización a JSON (Circular Reference) y visualización de datos sensibles.
        /// </remarks>
        [JsonIgnore]
        public ICollection<Directriz>? Directrices { get; set; }

        /// <summary>
        /// Obtiene o establece la colección de carreras asociados a la facultad.
        /// </summary>
        /// <remarks>
        /// El atributo [JsonIgnore] es crítico para evitar ciclos infinitos durante la 
        /// serialización a JSON (Circular Reference) y visualización de datos sensibles.
        /// </remarks>
        [JsonIgnore]
        public ICollection<Carrera>? Carreras { get; set; }
    }
}
