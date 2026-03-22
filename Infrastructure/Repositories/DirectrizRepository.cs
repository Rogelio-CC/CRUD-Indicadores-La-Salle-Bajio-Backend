using KPIBackend.Data;
using KPIBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace KPIBackend.Repositories
{
    /// <summary>
    /// Repositorio específico para la entidad Directriz.
    /// Extiende el repositorio base con consultas relacionadas a facultad y creador.
    /// </summary>
    public interface IDirectrizRepository : IBaseRepository<Directriz>
    {
        /// <summary>
        /// Obtiene las directrices asociadas a una facultad.
        /// </summary>
        /// <param name="facultadId">Identificador de la facultad.</param>
        /// <returns>Listado de directrices de la facultad.</returns>
        Task<IEnumerable<Directriz>> GetByFacultadAsync(Guid facultadId);

        /// <summary>
        /// Obtiene las directrices creadas por un usuario.
        /// </summary>
        /// <param name="creadorId">Identificador del usuario creador.</param>
        /// <returns>Listado de directrices del usuario.</returns>
        Task<IEnumerable<Directriz>> GetByCreadorAsync(Guid creadorId);
    }

    /// <summary>
    /// Implementación del repositorio de directrices.
    /// </summary>
    public class DirectrizRepository : BaseRepository<Directriz>, IDirectrizRepository
    {
        /// <summary>
        /// Inicializa el repositorio con el contexto de base de datos.
        /// </summary>
        /// <param name="context">Contexto de Entity Framework.</param>
        public DirectrizRepository(AppDbContext context) : base(context) { }

        /// <summary>
        /// Obtiene directrices filtradas por facultad.
        /// </summary>
        public async Task<IEnumerable<Directriz>> GetByFacultadAsync(Guid facultadId) =>
            await _dbSet.Where(d => d.FacultadId == facultadId).ToListAsync();

        /// <summary>
        /// Obtiene directrices filtradas por creador.
        /// </summary>
        public async Task<IEnumerable<Directriz>> GetByCreadorAsync(Guid creadorId) =>
            await _dbSet.Where(d => d.CreadorId == creadorId).ToListAsync();
    }
}
