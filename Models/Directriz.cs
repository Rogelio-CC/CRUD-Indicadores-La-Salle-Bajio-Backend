using System.Text.Json.Serialization;

namespace KPIBackend.Models
{
    /// <summary>
    /// Representa una directriz estratégica definida por una facultad
    /// para un periodo escolar específico.
    /// </summary>
    public class Directriz : IEntity, IOwnedEntity
    {
        /// <summary>
        /// Identificador único de la directriz.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Descripción de la directriz estratégica.
        /// </summary>
        public string Descripcion { get; set; } = null!;

        /// <summary>
        /// Clave foránea de la facultad a la que pertenece la directriz.
        /// </summary>
        public Guid FacultadId { get; set; }

        /// <summary>
        /// Navegación de la facultad.
        /// </summary>
        public Facultad Facultad { get; set; } = null!;

        /// <summary>
        /// Clave foránea del usuario que creó la directriz.
        /// </summary>
        public Guid CreadorId { get; set; }

        /// <summary>
        /// Navegación del usuario.
        /// </summary>
        public Usuario Creador { get; set; } = null!;

        /// <summary>
        /// Clave foránea del período escolar a la que pertenece la directriz.
        /// </summary>
        public Guid PeriodoId { get; set; }

        /// <summary>
        /// Navegación del período escolar.
        /// </summary>
        public PeriodoEscolar Periodo { get; set; } = null!;

        /// <summary>
        /// Clave foránea del comentario vinculado.
        /// </summary>
        public Guid? ComentarioId { get; set; }

        /// <summary>
        /// Navegación al comentario vinculado.
        /// </summary>
        public Comentario? Comentario { get; set; }

        /// <summary>
        /// Obtiene o establece la colección de indicadores asociados a la directriz.
        /// </summary>
        /// <remarks>
        /// El atributo [JsonIgnore] es crítico para evitar ciclos infinitos durante la 
        /// serialización a JSON (Circular Reference) y visualización de datos sensibles.
        /// </remarks>
        [JsonIgnore]
        public ICollection<Indicador>? Indicadores { get; set; }
    }
}
