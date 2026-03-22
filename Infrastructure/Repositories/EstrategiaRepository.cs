using KPIBackend.Data;
using KPIBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace KPIBackend.Repositories
{
    /// <summary>
    /// Repositorio específico para la entidad Estrategia.
    /// Añade consultas especializadas sobre el repositorio base.
    /// </summary>
    public interface IEstrategiaRepository : IBaseRepository<Estrategia>
    {
        /// <summary>
        /// Obtiene todas las estrategias asociadas a un indicador.
        /// </summary>
        /// <param name="indicadorId">ID del indicador.</param>
        /// <returns>Colección de estrategias relacionadas al indicador.</returns>
        Task<IEnumerable<Estrategia>> GetByIndicadorAsync(Guid indicadorId);

        /// <summary>
        /// Obtiene todas las estrategias creadas por un usuario.
        /// </summary>
        /// <param name="creadorId">ID del usuario creador.</param>
        /// <returns>Colección de estrategias del creador.</returns>
        Task<IEnumerable<Estrategia>> GetByCreadorAsync(Guid creadorId);
    }

    /// <summary>
    /// Implementación del repositorio de estrategia.
    /// </summary>
    public class EstrategiaRepository : BaseRepository<Estrategia>, IEstrategiaRepository
    {
        /// <summary>
        /// Inicializa el repositorio con el contexto de base de datos.
        /// </summary>
        /// <param name="context">Contexto de Entity Framework.</param>
        public EstrategiaRepository(AppDbContext context) : base(context) { }

        /// <summary>
        /// Obtiene estrategias filtradas por indicador.
        /// </summary>
        public async Task<IEnumerable<Estrategia>> GetByIndicadorAsync(Guid indicadorId) =>
            await _dbSet.Where(e => e.IndicadorId == indicadorId).ToListAsync();

        /// <summary>
        /// Obtiene estrategias filtradas por creador.
        /// </summary>
        public async Task<IEnumerable<Estrategia>> GetByCreadorAsync(Guid creadorId) =>
            await _dbSet.Where(e => e.IndicadorId == creadorId).ToListAsync();
    }
}
