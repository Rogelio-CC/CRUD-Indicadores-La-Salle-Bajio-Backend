using KPIBackend.Data;
using KPIBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace KPIBackend.Repositories
{
    /// <summary>
    /// Repositorio específico para la entidad Evidencia.
    /// Extiende el repositorio base con consultas por indicador.
    /// </summary>
    public interface IEvidenciaRepository : IBaseRepository<Evidencia>
    {
        /// <summary>
        /// Obtiene todas las evidencias asociadas a un indicador.
        /// </summary>
        /// <param name="indicadorId">ID del indicador.</param>
        /// <returns>Lista de evidencias del indicador.</returns>
        Task<IEnumerable<Evidencia>> GetByIndicadorAsync(Guid indicadorId);
    }

    /// <summary>
    /// Implementación del repositorio de evidencia.
    /// </summary>
    public class EvidenciaRepository : BaseRepository<Evidencia>, IEvidenciaRepository
    {
        /// <summary>
        /// Inicializa el repositorio con el contexto de base de datos.
        /// </summary>
        /// <param name="context">Contexto de Entity Framework.</param>
        public EvidenciaRepository(AppDbContext context) : base(context) { }

        /// <summary>
        /// Obtiene evidencias filtradas por indicador.
        /// </summary>
        public async Task<IEnumerable<Evidencia>> GetByIndicadorAsync(Guid indicadorId)
        {
            return await _dbSet
                .Where(e => e.IndicadorId == indicadorId)
                .ToListAsync();
        }
    }

}
