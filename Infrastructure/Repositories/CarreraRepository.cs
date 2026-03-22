using KPIBackend.Data;
using KPIBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace KPIBackend.Repositories
{
    /// <summary>
    /// Repositorio específico para operaciones sobre la entidad Carrera.
    /// Extiende el repositorio base con consultas especializadas.
    /// </summary>
    public interface ICarreraRepository : IBaseRepository<Carrera>
    {
        /// <summary>
        /// Obtiene todas las carreras pertenecientes a una facultad.
        /// </summary>
        /// <param name="facultadId">Identificador de la facultad.</param>
        /// <returns>Lista de carreras asociadas a la facultad.</returns>
        Task<IEnumerable<Carrera>> GetByFacultadAsync(Guid facultadId);
    }

    /// <summary>
    /// Implementación del repositorio de carrera.
    /// </summary>
    public class CarreraRepository : BaseRepository<Carrera>, ICarreraRepository
    {
        /// <summary>
        /// Inicializa el repositorio con el contexto de base de datos.
        /// </summary>
        /// <param name="context">Contexto de Entity Framework.</param>
        public CarreraRepository(AppDbContext context) : base(context) { }

        /// <summary>
        /// Obtiene carreras filtradas por facultad.
        /// </summary>
        public async Task<IEnumerable<Carrera>> GetByFacultadAsync(Guid facultadId) =>
            await _dbSet.Where(c => c.FacultadId == facultadId).ToListAsync();
    }
}
