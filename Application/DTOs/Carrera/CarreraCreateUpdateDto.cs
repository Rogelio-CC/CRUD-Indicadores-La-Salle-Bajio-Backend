namespace KPIBackend.Application.DTOs.Carrera
{
    /// <summary>
    /// DTO utilizado para crear o actualizar una carrera.
    /// </summary>
    public class CarreraCreateUpdateDto
    {
        /// <summary>
        /// Nombre de la carrera.
        /// </summary>
        public string NombreCarrera { get; set; } = null!;

        /// <summary>
        /// Identificador de la facultad a la que pertenece.
        /// </summary>
        public Guid FacultadId { get; set; }
    }
}
