namespace KPIBackend.Application.DTOs.Usuarios
{
    /// <summary>
    /// DTO para crear o actualizar un usuario.
    /// </summary>
    public class UsuarioCreateUpdateDto
    {
        /// <summary>
        /// Nombre del usuario.
        /// </summary>
        public string NombreUsuario { get; set; } = null!;

        /// <summary>
        /// Correo institucional del usuario.
        /// </summary>
        public string CorreoInstitucional { get; set; } = null!;

        /// <summary>
        /// Tipo de usuario.
        /// </summary>
        public string TipoUsuario { get; set; } = null!;

        /// <summary>
        /// Identificador del rol a la que pertenece el usuario.
        /// </summary>
        public Guid RolId { get; set; }

        /// <summary>
        /// Identificador de la facultad a la que pertenece el usuario.
        /// </summary>
        public Guid FacultadId { get; set; }

        /// <summary>
        /// Identificador de la carrera a la que pertenece el usuario.
        /// </summary>
        public Guid CarreraId { get; set; }


    }
}
