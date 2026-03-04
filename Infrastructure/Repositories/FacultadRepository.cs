using KPIBackend.Data;
using KPIBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace KPIBackend.Repositories
{
    public interface IFacultadRepository : IBaseRepository<Facultad>
    {
        Task<Facultad?> GetWithUsuariosAsync(Guid id);
    }

    public class FacultadRepository : BaseRepository<Facultad>, IFacultadRepository
    {
        public FacultadRepository(AppDbContext context) : base(context) { }

        public async Task<Facultad?> GetWithUsuariosAsync(Guid id) =>
            await _dbSet.Include(f => f.Usuarios)
                        .FirstOrDefaultAsync(f => f.Id == id);
    }
}
