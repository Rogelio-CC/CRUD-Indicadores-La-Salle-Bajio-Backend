using KPIBackend.Data;
using KPIBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace KPIBackend.Repositories
{
    public interface IBaseRepository<T> where T : class, IEntity
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsByNameAsync(string name);
        Task<bool> ExistsByNameAsyncExceptId(string name, Guid id);
        Task<bool> ExistsByGroupNumberAsync(int groupNumber, string description);
        Task<bool> ExistsByGroupNumberAsyncExceptId(int groupNumber, Guid id, string description);

    }

    public class BaseRepository<T> : IBaseRepository<T> where T : class, IEntity
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual async Task<T?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

        public virtual async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public virtual async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task UpdateAsync(T entity)
        {

            var existing = await _dbSet.FindAsync(entity.Id);
        
            if (existing == null) throw new Exception("Entidad no encontrada.");
        
            _context.Entry(existing).CurrentValues.SetValues(entity);
        
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                try
                {
                    _dbSet.Remove(entity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    throw new InvalidOperationException("FK_CONSTRAINT", ex);
                }
            }
        }

        public virtual async Task<bool> ExistsByNameAsync(string name)
        {
            return await _dbSet.Cast<IUniqueName>().AnyAsync(e => e.Nombre.ToLower() == name.ToLower());
        }

        public virtual async Task<bool> ExistsByNameAsyncExceptId(string name, Guid id)
        {
            return await _dbSet.Where(e => ((IUniqueName)e).Nombre.ToLower() == name.ToLower()).AnyAsync(e => e.Id != id);
        }

        public virtual async Task<bool> ExistsByGroupNumberAsync(int groupNumber, string description)
        {
            return await _dbSet.Cast<IUniqueNumber>().AnyAsync(e => e.numeroGrupo == groupNumber || e.DescripcionGrupo.ToLower() == description.ToLower());
        }

        public virtual async Task<bool> ExistsByGroupNumberAsyncExceptId(int groupNumber, Guid id, string description)
        {
            return await _dbSet.Where(e => ((IUniqueNumber)e).numeroGrupo == groupNumber || ((IUniqueNumber)e).DescripcionGrupo.ToLower() == description.ToLower()).AnyAsync(e => e.Id != id);
        }
    }

}
