using KPIBackend.Data;
using KPIBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace KPIBackend.Repositories
{
    /// <summary>
    /// Repositorio especializado en indicadores.
    /// Extiende el repositorio base con consultas relacionadas a directrices y creadores.
    /// </summary>
    public interface IIndicadorRepository : IBaseRepository<Indicador>
    {
        /// <summary>
        /// Obtiene los indicadores asociados a una directriz.
        /// </summary>
        /// <param name="directrizId">ID de la directriz.</param>
        /// <returns>Indicadores vinculados a la directriz.</returns>
        Task<IEnumerable<Indicador>> GetByDirectrizAsync(Guid directrizId);

        /// <summary>
        /// Obtiene los indicadores creados por un usuario.
        /// </summary>
        /// <param name="creadorId">ID del usuario creador.</param>
        /// <returns>Indicadores del creador.</returns>
        Task<IEnumerable<Indicador>> GetByCreadorAsync(Guid creadorId);
    }

    /// <summary>
    /// Implementación del repositorio de indicadores.
    /// </summary>
    public class IndicadorRepository : BaseRepository<Indicador>, IIndicadorRepository
    {
        /// <summary>
        /// Inicializa el repositorio con un DbContext.
        /// </summary>
        /// <param name="context">Contexto de Entity Framework.</param>
        public IndicadorRepository(AppDbContext context) : base(context) { }

        /// <summary>
        /// Obtiene indicadores filtrados por directriz.
        /// </summary>
        public async Task<IEnumerable<Indicador>> GetByDirectrizAsync(Guid directrizId) =>
            await _dbSet.Where(i => i.DirectrizId == directrizId).ToListAsync();

        /// <summary>
        /// Obtiene indicadores filtrados por creador.
        /// </summary>
        public async Task<IEnumerable<Indicador>> GetByCreadorAsync(Guid creadorId) =>
            await _dbSet.Where(i => i.CreadorId == creadorId).ToListAsync();
    }
}
