using KPIBackend.Data;
using KPIBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace KPIBackend.Repositories
{
    /// <summary>
    /// Repositorio específico para la entidad ArchivoPoliticas.
    /// Extiende el repositorio base con consultas por facultad.
    /// </summary>
    /// 
    public interface IArchivoPoliticasRepository : IBaseRepository<ArchivoPoliticas>
    {
        /// <summary>
        /// Obtiene el archivo de las politicas asociada a una facultad.
        /// </summary>
        /// <param name="facultadId">ID de la facultad.</param>
        /// <returns>Archivo de las políticas de la facultad.</returns>
        Task<IEnumerable<ArchivoPoliticas>> GetByFacultyAsync(Guid facultadId);
    }

    /// <summary>
    /// Implementación del repositorio del archivo de las políticas.
    /// </summary>
    public class ArchivoPoliticasRepository : BaseRepository<ArchivoPoliticas>, IArchivoPoliticasRepository
    {
        /// <summary>
        /// Inicializa el repositorio con el contexto de base de datos.
        /// </summary>
        /// <param name="context">Contexto de Entity Framework.</param>
        public ArchivoPoliticasRepository(AppDbContext context) : base(context) { }

        /// <summary>
        /// Obtiene el archivo filtrado por facultad.
        /// </summary>
        public async Task<IEnumerable<ArchivoPoliticas>> GetByFacultyAsync(Guid facultadId)
        {
            return await _dbSet
                .Where(e => e.FacultadId == facultadId)
                .ToListAsync();
        }
    }
}
