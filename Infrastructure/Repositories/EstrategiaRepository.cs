using KPIBackend.Data;
using KPIBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace KPIBackend.Repositories
{
    public interface IEstrategiaRepository : IBaseRepository<Estrategia>
    {
        Task<IEnumerable<Estrategia>> GetByIndicadorAsync(Guid indicadorId);
        Task<IEnumerable<Estrategia>> GetByCreadorAsync(Guid creadorId);
    }

    public class EstrategiaRepository : BaseRepository<Estrategia>, IEstrategiaRepository
    {
        public EstrategiaRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Estrategia>> GetByIndicadorAsync(Guid indicadorId) =>
            await _dbSet.Where(e => e.IndicadorId == indicadorId).ToListAsync();

        public async Task<IEnumerable<Estrategia>> GetByCreadorAsync(Guid creadorId) =>
            await _dbSet.Where(e => e.IndicadorId == creadorId).ToListAsync();
    }
}
