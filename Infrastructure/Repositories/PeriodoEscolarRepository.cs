using KPIBackend.Data;
using KPIBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace KPIBackend.Repositories
{
    /// <summary>
    /// Repositorio específico para la entidad PeriodoEscolar.
    /// Agrega consultas especializadas al repositorio base.
    /// </summary>
    public interface IPeriodoEscolarRepository : IBaseRepository<PeriodoEscolar>
    {
        /// <summary>
        /// Obtiene los periodos escolares que están activos en una fecha dada.
        /// </summary>
        /// <param name="fecha">Fecha de referencia para filtrar periodos activos.</param>
        /// <returns>Periodos escolares cuyo rango (inicio-fin) contiene la fecha.</returns>
        Task<IEnumerable<PeriodoEscolar>> GetPeriodosActivosAsync(DateTime fecha);
    }

    /// <summary>
    /// Implementación del repositorio de periodos escolares.
    /// </summary>
    public class PeriodoEscolarRepository : BaseRepository<PeriodoEscolar>, IPeriodoEscolarRepository
    {
        /// <summary>
        /// Inicializa el repositorio con el contexto de base de datos.
        /// </summary>
        /// <param name="context">Contexto de Entity Framework.</param>
        public PeriodoEscolarRepository(AppDbContext context) : base(context) { }

        /// <summary>
        /// Obtiene los periodos escolares activos en una fecha específica.
        /// </summary>
        public async Task<IEnumerable<PeriodoEscolar>> GetPeriodosActivosAsync(DateTime fecha) =>
            await _dbSet.Where(p => p.FechaInicio <= fecha && p.FechaFin >= fecha).ToListAsync();
    }
}
