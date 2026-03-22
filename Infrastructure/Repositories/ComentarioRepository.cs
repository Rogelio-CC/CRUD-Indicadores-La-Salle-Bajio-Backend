using KPIBackend.Data;
using KPIBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace KPIBackend.Repositories
{
    /// <summary>
    /// Repositorio específico para la entidad Comentario.
    /// Extiende IBaseRepository con consultas relacionadas a usuario y objetivo.
    /// </summary>
    public interface IComentarioRepository : IBaseRepository<Comentario>
    {
        /// <summary>
        /// Obtiene todos los comentarios creados por un usuario.
        /// </summary>
        /// <param name="usuarioId">ID del usuario creador.</param>
        /// <returns>Comentarios asociados al usuario.</returns>
        Task<IEnumerable<Comentario>> GetByUsuarioAsync(Guid usuarioId);

        /// <summary>
        /// Obtiene los comentarios asociados a un objetivo específico.
        /// </summary>
        /// <param name="objetivoId">ID del objetivo.</param>
        /// <param name="tipoObjetivo">Tipo del objetivo (por ejemplo: "directriz", "estrategia").</param>
        /// <returns>Comentarios relacionados con el objetivo indicado.</returns>
        Task<IEnumerable<Comentario>> GetByObjetivoAsync(Guid objetivoId, string tipoObjetivo);
    }

    /// <summary>
    /// Implementación del repositorio de comentarios.
    /// </summary>
    public class ComentarioRepository : BaseRepository<Comentario>, IComentarioRepository
    {
        /// <summary>
        /// Inicializa el repositorio con el contexto de base de datos.
        /// </summary>
        /// <param name="context">Contexto de Entity Framework.</param>
        public ComentarioRepository(AppDbContext context) : base(context) { }

        /// <summary>
        /// Obtiene comentarios realizados por un usuario.
        /// </summary>
        public async Task<IEnumerable<Comentario>> GetByUsuarioAsync(Guid usuarioId) =>
            await _dbSet.Where(c => c.CreadorId == usuarioId).ToListAsync();

        /// <summary>
        /// Obtiene comentarios para un objetivo y tipo de objetivo dados.
        /// </summary>
        public async Task<IEnumerable<Comentario>> GetByObjetivoAsync(Guid objetivoId, string tipoObjetivo) =>
            await _dbSet.Where(c => c.IdObjetivo == objetivoId && c.TipoObjetivo == tipoObjetivo).ToListAsync();
    }
}
