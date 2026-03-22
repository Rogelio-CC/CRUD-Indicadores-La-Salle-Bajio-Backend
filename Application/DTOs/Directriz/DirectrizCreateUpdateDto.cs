namespace KPIBackend.Application.DTOs.Directriz
{
    /// <summary>
    /// DTO para crear o actualizar directrices.
    /// </summary>
    public class DirectrizCreateUpdateDto
    {
        /// <summary>
        /// Descripción de la directriz.
        /// </summary>
        public string Descripcion { get; set; } = null!;

        /// <summary>
        /// Identificador de la facultad a la que pertenece la directriz.
        /// </summary>
        public Guid FacultadId { get; set; }

        /// <summary>
        /// Indetificador del usuario que creó la directriz.
        /// </summary>
        public Guid CreadorId { get; set; }

        /// <summary>
        /// Identificador del período escolar en el que aplica la directriz.
        /// </summary>
        public Guid PeriodoId { get; set; }
    }


}
