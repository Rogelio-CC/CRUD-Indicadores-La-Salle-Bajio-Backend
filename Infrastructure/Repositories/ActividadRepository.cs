using KPIBackend.Data;
using KPIBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace KPIBackend.Repositories
{
    public interface IActividadRepository : IBaseRepository<Actividad>
    {
        Task<IEnumerable<Actividad>> GetByEstrategiaAsync(Guid estrategiaId);
        Task<IEnumerable<Actividad>> GetByCreadorAsync(Guid creadorId);
        Task<bool> ExisteDuplicadaAsync(string descripcion, Guid periodoId, Guid estrategiaId);
    }

    public class ActividadRepository : BaseRepository<Actividad>, IActividadRepository
    {
        public ActividadRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Actividad>> GetByEstrategiaAsync(Guid estrategiaId) =>
            await _dbSet.Where(a => a.EstrategiaId == estrategiaId).ToListAsync();

        public async Task<IEnumerable<Actividad>> GetByCreadorAsync(Guid creadorId) =>
            await _dbSet.Where(a => a.CreadorId == creadorId).ToListAsync();

        public async Task<bool> ExisteDuplicadaAsync(string descripcion, Guid periodoId, Guid estrategiaId) =>
            await _dbSet.AnyAsync(a => a.DescripcionActividad == descripcion
                                    && a.PeriodoId == periodoId
                                    && a.EstrategiaId == estrategiaId);
    }
}
