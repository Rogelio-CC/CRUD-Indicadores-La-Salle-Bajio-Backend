using KPIBackend.Data;
using KPIBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace KPIBackend.Repositories
{
    public interface IComentarioRepository : IBaseRepository<Comentario>
    {
        Task<IEnumerable<Comentario>> GetByUsuarioAsync(Guid usuarioId);
        Task<IEnumerable<Comentario>> GetByObjetivoAsync(Guid objetivoId, string tipoObjetivo);
    }

    public class ComentarioRepository : BaseRepository<Comentario>, IComentarioRepository
    {
        public ComentarioRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Comentario>> GetByUsuarioAsync(Guid usuarioId) =>
            await _dbSet.Where(c => c.CreadorId == usuarioId).ToListAsync();

        public async Task<IEnumerable<Comentario>> GetByObjetivoAsync(Guid objetivoId, string tipoObjetivo) =>
            await _dbSet.Where(c => c.IdObjetivo == objetivoId && c.TipoObjetivo == tipoObjetivo).ToListAsync();
    }
}
