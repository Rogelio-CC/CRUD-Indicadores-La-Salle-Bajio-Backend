using System.Text.Json.Serialization;

namespace KPIBackend.Models
{
    /// <summary>
    /// Representa una estrategia diseñada para cumplir un indicador.
    /// </summary>
    public class Estrategia : IEntity, IOwnedEntity
    {
        /// <summary>
        /// Identificador único de la estrategia.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Descripción de la estrategia.
        /// </summary>
        public string DescripcionEstrategia { get; set; } = null!;

        /// <summary>
        /// Fecha en que se emitió la estrategia.
        /// </summary>
        public DateTime FechaEmision { get; set; }

        /// <summary>
        /// Clave foránea del indicador a la que pertenece la estrategia.
        /// </summary>
        public Guid IndicadorId { get; set; }

        /// <summary>
        /// Navegación del indicador.
        /// </summary>
        public Indicador Indicador { get; set; } = null!;

        /// <summary>
        /// Clave foránea del usuario que creó la estrategia.
        /// </summary>
        public Guid CreadorId { get; set; }

        /// <summary>
        /// Navegación del usuario.
        /// </summary>
        public Usuario Creador { get; set; } = null!;

        /// <summary>
        /// Clave foránea del período escolar a la que aplica la estrategia.
        /// </summary>
        public Guid PeriodoId { get; set; }

        /// <summary>
        /// Navegación del período escolar.
        /// </summary>
        public PeriodoEscolar Periodo { get; set; } = null!;

        /// <summary>
        /// Clave foránea de la carrera a la que pertenece la estrategia.
        /// </summary>
        public Guid CarreraId { get; set; }

        /// <summary>
        /// Navegación del carrera.
        /// </summary>
        public Carrera Carrera { get; set; } = null!;

        /// <summary>
        /// Obtiene o establece la colección de actividades asociados a la estrategia.
        /// </summary>
        /// <remarks>
        /// El atributo [JsonIgnore] es crítico para evitar ciclos infinitos durante la 
        /// serialización a JSON (Circular Reference) y visualización de datos sensibles.
        /// </remarks>
        [JsonIgnore]
        public ICollection<Actividad>? Actividades { get; set; }

        /// <summary>
        /// Obtiene o establece la colección de comentarios asociados a la estrategia.
        /// </summary>
        /// <remarks>
        /// El atributo [JsonIgnore] es crítico para evitar ciclos infinitos durante la 
        /// serialización a JSON (Circular Reference) y visualización de datos sensibles.
        /// </remarks>
        [JsonIgnore]
        public ICollection<Comentario>? Comentarios { get; set; }
    }
}
