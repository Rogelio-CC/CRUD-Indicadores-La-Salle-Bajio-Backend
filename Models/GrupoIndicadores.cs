using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KPIBackend.Models
{
    /// <summary>
    /// Representa un grupo de indicadores utilizado para su organización.
    /// </summary>
    public class GrupoIndicadores : IEntity, IUniqueNumber
    {
        /// <summary>
        /// Identificador único del grupo de indicadores.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Número del grupo de indicadores (no es lo mismo que el su Id; sirve para ser otro identificador pero más entendible que un Id).
        /// </summary>
        [Required(ErrorMessage = "El número del grupo es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "El valor no puede ser negativo.")]
        public int NumeroGrupo { get; set; }

        /// <summary>
        /// Descripción del grupo de indicadores
        /// </summary>
        [Required(ErrorMessage = "La descripción es obligatorio")]
        [MinLength(5, ErrorMessage = "Debe tener al menos 5 caracteres")]
        [MaxLength(100, ErrorMessage = "No puede exceder 100 caracteres")]
        public string DescripcionGrupo { get; set; } = null!;

        /// <summary>
        /// Obtiene o establece la colección de indicadores asociados al grupo de indicadores.
        /// </summary>
        /// <remarks>
        /// El atributo [JsonIgnore] es crítico para evitar ciclos infinitos durante la 
        /// serialización a JSON (Circular Reference) y visualización de datos sensibles.
        /// </remarks>
        [JsonIgnore]
        public ICollection<Indicador>? Indicadores { get; set; }
    }
}
