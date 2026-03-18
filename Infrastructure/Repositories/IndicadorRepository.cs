using KPIBackend.Data;
using KPIBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace KPIBackend.Repositories
{
    public interface IIndicadorRepository : IBaseRepository<Indicador>
    {
        Task<IEnumerable<Indicador>> GetByDirectrizAsync(Guid directrizId);
        Task<IEnumerable<Indicador>> GetByCreadorAsync(Guid creadorId);
        //Task<double> GetPorcentajeCumplimientoAsync(Guid idIndicador);
    }

    public class IndicadorRepository : BaseRepository<Indicador>, IIndicadorRepository
    {
        public IndicadorRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Indicador>> GetByDirectrizAsync(Guid directrizId) =>
            await _dbSet.Where(i => i.DirectrizId == directrizId).ToListAsync();

        public async Task<IEnumerable<Indicador>> GetByCreadorAsync(Guid creadorId) =>
            await _dbSet.Where(i => i.CreadorId == creadorId).ToListAsync();

        //public async Task<double> GetPorcentajeCumplimientoAsync(Guid idIndicador)
        //{
        //    var actividades = await (from a in _context.Set<Actividad>()
        //                             join e in _context.Set<Estrategia>() on a.IdEstrategia equals e.Id
        //                             where e.IdIndicador == idIndicador
        //                             select a).ToListAsync();

        //    if (!actividades.Any())
        //        return 0.0;

        //    double total = actividades.Count;
        //    double cumplidas = actividades.Count(a => a.Cumplida);
        //    return (cumplidas / total) * 100.0;
        //}
    }
}
