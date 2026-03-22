namespace KPIBackend.Application.DTOs.Carrera
{
    /// <summary>
    /// DTO que representa una carrera y su facultad asociada.
    /// </summary>
    public class CarreraDto
    {
        /// <summary>
        /// Identificador de la carrera.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nombre de la carrera.
        /// </summary>
        public string NombreCarrera { get; set; } = null!;

        /// <summary>
        /// Identificador de la facultad a la que pertenece.
        /// </summary>
        public Guid FacultadId { get; set; }

         /// <summary>
        /// Nombre de la facultad.
        /// </summary>
        public string Facultad { get; set; } = string.Empty;
    }
}
