using KPIBackend.Data;
using KPIBackend.Models;
using KPIBackend.Repositories;
using Microsoft.EntityFrameworkCore;

namespace KPIBackend.Infrastructure.Repositories
{
    public interface IEvidenciaRepository : IBaseRepository<Evidencia>
    {
        Task<IEnumerable<Evidencia>> GetByIndicadorAsync(Guid indicadorId);
    }

    public class EvidenciaRepository : BaseRepository<Evidencia>, IEvidenciaRepository
    {
        public EvidenciaRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Evidencia>> GetByIndicadorAsync(Guid indicadorId)
        {
            return await _dbSet
                .Where(e => e.IndicadorId == indicadorId)
                .ToListAsync();
        }
    }

}
