using KPIBackend.Models;

namespace KPIBackend.Application.DTOs.Usuarios
{
    public class UsuarioDto
    {
        public Guid Id { get; set; }
        public string NombreUsuario { get; set; } = null!;
        public string CorreoInstitucional { get; set; } = null!;
        public string TipoUsuario { get; set; } = null!;

        public Guid RolId { get; set; }
        public string Rol { get; set; } = string.Empty!;

        public Guid FacultadId { get; set; }
        public string Facultad { get; set; } = string.Empty!;

        public Guid CarreraId { get; set; }
        public string Carrera { get; set; } = string.Empty!;
    }
}
