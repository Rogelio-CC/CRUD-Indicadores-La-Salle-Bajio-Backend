using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KPIBackend.Models
{
    /// <summary>
    /// Representa un rol dentro del sistema que define los permisos
    /// asignados a un usuario.
    /// </summary>
    public class Rol : IEntity, IUniqueName
    {
        /// <summary>
        /// Identificador único del rol.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nombre del rol.
        /// </summary>
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MinLength(5, ErrorMessage = "Debe tener al menos 5 caracteres")]
        [MaxLength(100, ErrorMessage = "No puede exceder 100 caracteres")]
        public string Nombre { get; set; } = null!;

        /// <summary>
        /// Obtiene o establece la colección de usuarios asociados al rol.
        /// </summary>
        /// <remarks>
        /// El atributo [JsonIgnore] es crítico para evitar ciclos infinitos durante la 
        /// serialización a JSON (Circular Reference) y visualización de datos sensibles.
        /// </remarks>
        [JsonIgnore]
        public ICollection<Usuario>? Usuarios { get; set; }
    }
}
