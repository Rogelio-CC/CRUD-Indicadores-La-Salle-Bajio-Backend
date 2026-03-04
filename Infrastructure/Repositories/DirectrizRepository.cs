using KPIBackend.Data;
using KPIBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace KPIBackend.Repositories
{
    public interface IDirectrizRepository : IBaseRepository<Directriz>
    {
        Task<IEnumerable<Directriz>> GetByFacultadAsync(Guid facultadId);
        Task<IEnumerable<Directriz>> GetByCreadorAsync(Guid creadorId);
    }

    public class DirectrizRepository : BaseRepository<Directriz>, IDirectrizRepository
    {
        public DirectrizRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Directriz>> GetByFacultadAsync(Guid facultadId) =>
            await _dbSet.Where(d => d.FacultadId == facultadId).ToListAsync();

        public async Task<IEnumerable<Directriz>> GetByCreadorAsync(Guid creadorId) =>
            await _dbSet.Where(d => d.CreadorId == creadorId).ToListAsync();
    }
}
