namespace KPIBackend.Application.DTOs.Usuarios
{
    /// <summary>
    /// DTO para representar un usuario.
    /// </summary>
    public class UsuarioDto
    {
        /// <summary>
        /// Identificador del usuario.
        /// </summary>
        public Guid Id { get; set; }

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
        /// Nombre del rol.
        /// </summary>
        public string Rol { get; set; } = string.Empty!;

        /// <summary>
        /// Identificador de la facultad a la que pertenece el usuario.
        /// </summary>
        public Guid FacultadId { get; set; }

        /// <summary>
        /// Nombre de la facultad.
        /// </summary>
        public string Facultad { get; set; } = string.Empty!;

        /// <summary>
        /// Identificador de la carrera a la que pertenece el usuario.
        /// </summary>
        public Guid CarreraId { get; set; }

        /// <summary>
        /// Nombre de la carrera.
        /// </summary>
        public string Carrera { get; set; } = string.Empty!;
    }
}
