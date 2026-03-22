using KPIBackend.Data;
using KPIBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace KPIBackend.Repositories
{
    /// <summary>
    /// Repositorio específico para la entidad Facultad.
    /// Añade consultas especializadas al repositorio base.
    /// </summary>
    public interface IFacultadRepository : IBaseRepository<Facultad>
    {
        /// <summary>
        /// Obtiene una facultad junto con sus usuarios relacionados.
        /// </summary>
        /// <param name="id">ID de la facultad.</param>
        /// <returns>Facultad con lista de usuarios o null si no existe.</returns>
        Task<Facultad?> GetWithUsuariosAsync(Guid id);
    }

    /// <summary>
    /// Implementación del repositorio para facultades.
    /// </summary>
    public class FacultadRepository : BaseRepository<Facultad>, IFacultadRepository
    {
        /// <summary>
        /// Inicializa el repositorio con el contexto de base de datos.
        /// </summary>
        /// <param name="context">Contexto de Entity Framework.</param>
        public FacultadRepository(AppDbContext context) : base(context) { }

        /// <summary>
        /// Obtiene una facultad por ID, incluyendo la colección de usuarios relacionada.
        /// </summary>
        public async Task<Facultad?> GetWithUsuariosAsync(Guid id) =>
            await _dbSet.Include(f => f.Usuarios)
                        .FirstOrDefaultAsync(f => f.Id == id);
    }
}
