using KPIBackend.Data;
using KPIBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace KPIBackend.Repositories
{
    /// <summary>
    /// Repositorio específico para la entidad Usuario.
    /// Agrega consultas especializadas sobre el repositorio base.
    /// </summary>
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        /// <summary>
        /// Obtiene los usuarios que coinciden con un rol específico.
        /// </summary>
        /// <param name="rol">Nombre del rol (por ejemplo: "Admin", "Docente").</param>
        /// <returns>Usuarios con ese rol.</returns>
        Task<IEnumerable<Usuario>> GetByRolAsync(string rol);

        /// <summary>
        /// Obtiene un usuario por su correo institucional.
        /// </summary>
        /// <param name="correo">Correo institucional del usuario.</param>
        /// <returns>Usuario si existe, null en caso contrario.</returns>
        Task<Usuario?> GetByCorreoAsync(string correo);
    }

    /// <summary>
    /// Implementación del repositorio de usuarios.
    /// </summary>
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        /// <summary>
        /// Inicializa el repositorio con el contexto de base de datos.
        /// </summary>
        /// <param name="context">Contexto de Entity Framework.</param>
        public UsuarioRepository(AppDbContext context) : base(context) { }

        /// <summary>
        /// Obtiene usuarios filtrados por rol.
        /// </summary>
        public async Task<IEnumerable<Usuario>> GetByRolAsync(string rol) =>
            await _dbSet.Where(u => u.TipoUsuario == rol).ToListAsync();

        /// <summary>
        /// Obtiene un usuario por correo institucional.
        /// </summary>
        public async Task<Usuario?> GetByCorreoAsync(string correo) =>
            await _dbSet.FirstOrDefaultAsync(u => u.CorreoInstitucional == correo);
    }
}
