using KPIBackend.Data;
using KPIBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace KPIBackend.Repositories
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        Task<IEnumerable<Usuario>> GetByRolAsync(string rol);
        Task<Usuario?> GetByCorreoAsync(string correo);
    }

    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Usuario>> GetByRolAsync(string rol) =>
            await _dbSet.Where(u => u.TipoUsuario == rol).ToListAsync();

        public async Task<Usuario?> GetByCorreoAsync(string correo) =>
            await _dbSet.FirstOrDefaultAsync(u => u.CorreoInstitucional == correo);
    }
}
