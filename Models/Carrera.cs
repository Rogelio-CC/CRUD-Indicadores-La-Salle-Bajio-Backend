using System.Text.Json.Serialization;

namespace KPIBackend.Models
{
    /// <summary>
    /// Representa una carrera académica dentro de la facultad.
    /// </summary>
    public class Carrera : IEntity
    {
        /// <summary>
        /// Identificador único de la carrera.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nombre de la carrera (por ejemplo: "Ingeniería en Sistemas").
        /// </summary>
        public string NombreCarrera { get; set; } = null!;

        /// <summary>
        /// Clave foránea a la facultad a la que pertenece.
        /// </summary>
        public Guid FacultadId { get; set; }

        /// <summary>
        /// Navegación a la facultad asociada.
        /// </summary>
        public Facultad Facultad { get; set; } = null!;

        /// <summary>
        /// Obtiene o establece la colección de usuarios asociados a la carrera.
        /// </summary>
        /// <remarks>
        /// El atributo [JsonIgnore] es crítico para evitar ciclos infinitos durante la 
        /// serialización a JSON (Circular Reference) y visualización de datos sensibles.
        /// </remarks>
        [JsonIgnore]
        public ICollection<Usuario>? Usuarios { get; set; }

        /// <summary>
        /// Obtiene o establece la colección de indicadores asociados a la carrera.
        /// </summary>
        /// <remarks>
        /// El atributo [JsonIgnore] es crítico para evitar ciclos infinitos durante la 
        /// serialización a JSON (Circular Reference) y visualización de datos sensibles.
        /// </remarks>
        [JsonIgnore]
        public ICollection<Indicador>? Indicadores { get; set; }

        /// <summary>
        /// Obtiene o establece la colección de estrategias asociados a la carrera.
        /// </summary>
        /// <remarks>
        /// El atributo [JsonIgnore] es crítico para evitar ciclos infinitos durante la 
        /// serialización a JSON (Circular Reference) y visualización de datos sensibles.
        /// </remarks>
        [JsonIgnore]
        public ICollection<Estrategia>? Estrategias { get; set; }

        /// <summary>
        /// Obtiene o establece la colección de actividades asociados a la carrera.
        /// </summary>
        /// <remarks>
        /// El atributo [JsonIgnore] es crítico para evitar ciclos infinitos durante la 
        /// serialización a JSON (Circular Reference) y visualización de datos sensibles.
        /// </remarks>
        [JsonIgnore]
        public ICollection<Actividad>? Actividades { get; set; }
    }
}
