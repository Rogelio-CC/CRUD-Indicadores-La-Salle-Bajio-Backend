namespace KPIBackend.Application.DTOs.Directriz
{
    /// <summary>
    /// DTO que representa una directriz institucional.
    /// </summary>
    public class DirectrizDto
    {
        /// <summary>
        /// Identificador de la directriz.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Descripción de la directriz estratégica.
        /// </summary>
        public string Descripcion { get; set; } = string.Empty;

        /// <summary>
        /// Identificador de la facultad a la que pertenece la directriz.
        /// </summary>
        public Guid FacultadId { get; set; }

        /// <summary>
        /// Nombre de la facultad.
        /// </summary>
        public string Facultad { get; set; } = string.Empty;

        /// <summary>
        /// Indetificador del usuario que creó la directriz.
        /// </summary>
        public Guid CreadorId { get; set; }

        /// <summary>
        /// Nombre del usuario.
        /// </summary>
        public string Creador { get; set; } = string.Empty;

        /// <summary>
        /// Identificador del período escolar en el que aplica la directriz.
        /// </summary>
        public Guid PeriodoId { get; set; }

        /// <summary>
        /// Nombre de período escolar.
        /// </summary>
        public string Periodo { get; set; } = string.Empty;
    }
}
