using KPIBackend.Data;
using KPIBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace KPIBackend.Repositories
{
    public interface IPeriodoEscolarRepository : IBaseRepository<PeriodoEscolar>
    {
        Task<IEnumerable<PeriodoEscolar>> GetPeriodosActivosAsync(DateTime fecha);
    }

    public class PeriodoEscolarRepository : BaseRepository<PeriodoEscolar>, IPeriodoEscolarRepository
    {
        public PeriodoEscolarRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<PeriodoEscolar>> GetPeriodosActivosAsync(DateTime fecha) =>
            await _dbSet.Where(p => p.FechaInicio <= fecha && p.FechaFin >= fecha).ToListAsync();
    }
}
