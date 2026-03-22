using System.Text.Json.Serialization;

namespace KPIBackend.Models
{
    /// <summary>
    /// Representa un usuario del sistema con un rol asignado
    /// dentro de una facultad y carrera.
    /// </summary>
    public class Usuario : IEntity
    {
        /// <summary>
        /// Identificador único del usuario.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nombre del usuario.
        /// </summary>
        public string NombreUsuario { get; set; } = null!;

        /// <summary>
        /// Correo institucional utilizado para autenticación.
        /// </summary>
        public string CorreoInstitucional { get; set; } = null!;

        /// <summary>
        /// Tipo de usuarios utilizado para la autorización (relacionado fuertemente al rol).
        /// </summary>
        public string TipoUsuario { get; set; } = null!;

        /// <summary>
        /// Clave foránea del rol a la que aplica el usuario.
        /// </summary>
        public Guid RolId { get; set; }

        /// <summary>
        /// Navegación del rol.
        /// </summary>
        public Rol Rol { get; set; } = null!;

        /// <summary>
        /// Clave foránea de la facultad a la que pertenece el usuario.
        /// </summary>
        public Guid FacultadId { get; set; }

        /// <summary>
        /// Navegación de la facultad.
        /// </summary>
        public Facultad Facultad { get; set; } = null!;

        /// <summary>
        /// Clave foránea de la carrera a la que pertenece el usuario.
        /// </summary>
        public Guid CarreraId { get; set; }

        /// <summary>
        /// Navegación de la carrera.
        /// </summary>
        public Carrera Carrera { get; set; } = null!;

        /// <summary>
        /// Obtiene o establece la colección de directrices asociados al usuario.
        /// </summary>
        /// <remarks>
        /// El atributo [JsonIgnore] es crítico para evitar ciclos infinitos durante la 
        /// serialización a JSON (Circular Reference) y visualización de datos sensibles.
        /// </remarks>
        [JsonIgnore]
        public ICollection<Directriz>? DirectricesCreadas { get; set; }

        /// <summary>
        /// Obtiene o establece la colección de estrategias asociados al usuario.
        /// </summary>
        /// <remarks>
        /// El atributo [JsonIgnore] es crítico para evitar ciclos infinitos durante la 
        /// serialización a JSON (Circular Reference) y visualización de datos sensibles.
        /// </remarks>
        [JsonIgnore]
        public ICollection<Estrategia>? EstrategiasCreadas { get; set; }

        /// <summary>
        /// Obtiene o establece la colección de actividades asociados al usuario.
        /// </summary>
        /// <remarks>
        /// El atributo [JsonIgnore] es crítico para evitar ciclos infinitos durante la 
        /// serialización a JSON (Circular Reference) y visualización de datos sensibles.
        /// </remarks>
        [JsonIgnore]
        public ICollection<Actividad>? ActividadesCreadas { get; set; }

        /// <summary>
        /// Obtiene o establece la colección de comentarios asociados al usuario.
        /// </summary>
        /// <remarks>
        /// El atributo [JsonIgnore] es crítico para evitar ciclos infinitos durante la 
        /// serialización a JSON (Circular Reference) y visualización de datos sensibles.
        /// </remarks>
        [JsonIgnore]
        public ICollection<Comentario>? Comentarios { get; set; }
    }
}
