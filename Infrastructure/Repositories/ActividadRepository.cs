using KPIBackend.Data;
using KPIBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace KPIBackend.Repositories
{
    /// <summary>
    /// Contrato para operaciones de acceso a datos de actividades.
    /// Extiende IBaseRepository con consultas especializadas para Actividad.
    /// </summary>
    public interface IActividadRepository : IBaseRepository<Actividad>
    {
        /// <summary>
        /// Obtiene todas las actividades asociadas a una estrategia específica.
        /// </summary>
        /// <param name="estrategiaId">Identificador único de la estrategia.</param>
        /// <returns>Colección de actividades de la estrategia.</returns>
        Task<IEnumerable<Actividad>> GetByEstrategiaAsync(Guid estrategiaId);

        /// <summary>
        /// Obtiene todas las actividades creadas por un usuario específico.
        /// </summary>
        /// <param name="creadorId">Identificador único del usuario creador.</param>
        /// <returns>Colección de actividades del usuario.</returns>
        Task<IEnumerable<Actividad>> GetByCreadorAsync(Guid creadorId);

        /// <summary>
        /// Verifica si existe una actividad duplicada con la misma descripción
        /// en un periodo escolar y estrategia específicos.
        /// </summary>
        /// <param name="descripcion">Descripción de la actividad a verificar.</param>
        /// <param name="periodoId">Identificador del periodo escolar.</param>
        /// <param name="estrategiaId">Identificador de la estrategia.</param>
        /// <returns>Verdadero si existe duplicado, falso en caso contrario.</returns>
        Task<bool> ExisteDuplicadaAsync(string descripcion, Guid periodoId, Guid estrategiaId);
    }

    /// <summary>
    /// Implementación del repositorio de Actividad.
    /// Proporciona acceso a datos y operaciones CRUD para actividades.
    /// </summary>
    public class ActividadRepository : BaseRepository<Actividad>, IActividadRepository
    {
        /// <summary>
        /// Constructor que inicializa el repositorio con el contexto de base de datos.
        /// </summary>
        /// <param name="context">Contexto de Entity Framework para acceso a datos.</param>
        public ActividadRepository(AppDbContext context) : base(context) { }

        /// <summary>
        /// Obtiene actividades filtradas por estrategia.
        /// </summary>
        public async Task<IEnumerable<Actividad>> GetByEstrategiaAsync(Guid estrategiaId) =>
            await _dbSet.Where(a => a.EstrategiaId == estrategiaId).ToListAsync();

        /// <summary>
        /// Obtiene actividades filtradas por creador.
        /// </summary>
        public async Task<IEnumerable<Actividad>> GetByCreadorAsync(Guid creadorId) =>
            await _dbSet.Where(a => a.CreadorId == creadorId).ToListAsync();

        /// <summary>
        /// Verifica duplicación por descripción en el mismo periodo y estrategia.
        /// </summary>
        public async Task<bool> ExisteDuplicadaAsync(string descripcion, Guid periodoId, Guid estrategiaId) =>
            await _dbSet.AnyAsync(a => a.DescripcionActividad == descripcion
                                    && a.PeriodoId == periodoId
                                    && a.EstrategiaId == estrategiaId);
    }
}
