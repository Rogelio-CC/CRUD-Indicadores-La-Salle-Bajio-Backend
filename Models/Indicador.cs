using System.Text.Json.Serialization;

namespace KPIBackend.Models
{
    /// <summary>
    /// Representa un indicador de desempeño utilizado para medir el cumplimiento
    /// de una directriz estratégica.
    /// </summary>  
    public class Indicador : IEntity, IOwnedEntity
    {
        /// <summary>
        /// Identificador único del indicador.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Descripción del indicador.
        /// </summary>
        public string DescripcionIndicador { get; set; } = null!;

        /// <summary>
        /// Estándar (porcentaje) del indicador.
        /// </summary>
        public decimal Estandar { get; set; } = 0;

        /// <summary>
        /// Frecuencia de control (cada cuando se administra el indicador: mensual, semanal, diario, anual).
        /// </summary>
        public string FrecuenciaControl { get; set; } = null!;

        /// <summary>
        /// Cantidad de evidencias requeridas.
        /// </summary>
        public int CantidadEvidencias { get; set; }

        /// <summary>
        /// Indica si el indicador está completado.
        /// </summary>
        public bool IndicadorCompletado { get; set; }

        /// <summary>
        /// Acción correctiva opcional.
        /// </summary>
        public string? AccionCorrectiva { get; set; }

        /// <summary>
        /// Fecha en la que se emite el indicador.
        /// </summary>
        public DateTime FechaEmision { get; set; }

        private DateTimeOffset? _fechaCumplimiento;

        /// <summary>
        /// Fecha en la que se cumplió el indicador.
        /// </summary>
        public DateTimeOffset? FechaCumplimiento
        {
            get => _fechaCumplimiento;
            set => _fechaCumplimiento = value;
        }

        /// <summary>
        /// Clave foránea de la directriz a la que aplica el indicador.
        /// </summary>
        public Guid DirectrizId { get; set; }

        /// <summary>
        /// Navegación de la directriz.
        /// </summary>
        public Directriz Directriz { get; set; } = null!;

        /// <summary>
        /// Clave foránea del grupo a la que aplica el indicador.
        /// </summary>
        public Guid GrupoId { get; set; }

        /// <summary>
        /// Navegación del grupo.
        /// </summary>
        public GrupoIndicadores Grupo { get; set; } = null!;

        /// <summary>
        /// Clave foránea del usuario que creó el indicador.
        /// </summary>
        public Guid CreadorId { get; set; }

        /// <summary>
        /// Navegación del usuario.
        /// </summary>
        public Usuario Creador { get; set; } = null!;

        /// <summary>
        /// Clave foránea opcional del usuario responsable a la que aplica el indicador.
        /// </summary>
        public Guid? ResponsableAccionCorrectivaId { get; set; }

        /// <summary>
        /// Navegación opcional del usuario responsable a la que aplica el indicador.
        /// </summary>
        public Usuario? ResponsableAccionCorrectiva { get; set; }

        /// <summary>
        /// Clave foránea opcional del período escolar a la que pertenece el indicador.
        /// </summary>
        public Guid PeriodoId { get; set; }

        /// <summary>
        /// Navegación del período escolar.
        /// </summary>
        public PeriodoEscolar Periodo { get; set; } = null!;

        /// <summary>
        /// Clave foránea de la carrera a la que aplica el indicador.
        /// </summary>
        public Guid CarreraId { get; set; }

        /// <summary>
        /// Navegación de la carrera.
        /// </summary>
        public Carrera Carrera { get; set; } = null!;

        /// <summary>
        /// Obtiene o establece la colección de evidencias asociados al indicador.
        /// </summary>
        /// <remarks>
        /// El atributo [JsonIgnore] es crítico para evitar ciclos infinitos durante la 
        /// serialización a JSON (Circular Reference) y visualización de datos sensibles.
        /// </remarks>
        [JsonIgnore]
        public ICollection<Evidencia> Evidencias { get; set; } = new List<Evidencia>();

        /// <summary>
        /// Obtiene o establece la colección de estrategias asociados al indicador.
        /// </summary>
        /// <remarks>
        /// El atributo [JsonIgnore] es crítico para evitar ciclos infinitos durante la 
        /// serialización a JSON (Circular Reference) y visualización de datos sensibles.
        /// </remarks>
        [JsonIgnore]
        public ICollection<Estrategia>? Estrategias { get; set; }
    }
}
