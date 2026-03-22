using KPIBackend.Data;
using KPIBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace KPIBackend.Repositories
{
    /// <summary>
    /// Contrato genérico para operaciones de acceso a datos (CRUD) y validaciones de unicidad.
    /// </summary>
    /// <typeparam name="T">Tipo de entidad que implementa IEntity.</typeparam>
    public interface IBaseRepository<T> where T : class, IEntity
    {
        /// <summary>
        /// Obtiene una entidad por su identificador único.
        /// </summary>
        /// <param name="id">Identificador GUID de la entidad.</param>
        /// <returns>La entidad encontrada o null si no existe.</returns>
        Task<T?> GetByIdAsync(Guid id);

        /// <summary>
        /// Obtiene todas las entidades del tipo T.
        /// </summary>
        /// <returns>Colección de todas las entidades.</returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Agrega una nueva entidad a la base de datos.
        /// </summary>
        /// <param name="entity">Entidad a añadir.</param>
        /// <returns>Entidad añadida con los cambios guardados.</returns>
        Task<T> AddAsync(T entity);

        /// <summary>
        /// Actualiza una entidad existente en la base de datos.
        /// </summary>
        /// <param name="entity">Entidad con valores actualizados.</param>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Elimina una entidad por su identificador.
        /// </summary>
        /// <param name="id">Identificador GUID de la entidad a eliminar.</param>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Verifica si existe una entidad con un nombre único (sin distinguir mayúsculas/minúsculas).
        /// Solo aplica a entidades que implementan IUniqueName.
        /// </summary>
        /// <param name="name">Nombre a verificar.</param>
        /// <returns>Verdadero si el nombre ya existe en otra entidad, falso en caso contrario.</returns>
        Task<bool> ExistsByNameAsync(string name);

        /// <summary>
        /// Verifica si existe una entidad con un nombre único, excluyendo una entidad específica por ID.
        /// Útil para validar cambios de nombre sin considerar la entidad actual. Solo para entidades que implementan IUniqueName.
        /// </summary>
        /// <param name="name">Nombre a verificar.</param>
        /// <param name="id">ID de la entidad a excluir de la verificación.</param>
        /// <returns>Verdadero si el nombre existe en otra entidad, falso en caso contrario.</returns>
        Task<bool> ExistsByNameAsyncExceptId(string name, Guid id);

        /// <summary>
        /// Verifica si existe un número de grupo único o una descripción duplicada (sin distinguir mayúsculas/minúsculas).
        /// Solo aplica a entidades que implementan IUniqueNumber.
        /// </summary>
        /// <param name="groupNumber">Número del grupo a verificar.</param>
        /// <param name="description">Descripción del grupo a verificar.</param>
        /// <returns>Verdadero si el número o descripción ya existen, falso en caso contrario.</returns>
        Task<bool> ExistsByGroupNumberAsync(int groupNumber, string description);

        /// <summary>
        /// Verifica si existe un número de grupo único o descripción, excluyendo una entidad específica por ID.
        /// Solo para entidades que implementan IUniqueNumber.
        /// </summary>
        /// <param name="groupNumber">Número del grupo a verificar.</param>
        /// <param name="id">ID de la entidad a excluir de la verificación.</param>
        /// <param name="description">Descripción del grupo a verificar.</param>
        /// <returns>Verdadero si el número o descripción existen en otra entidad, falso en caso contrario.</returns>
        Task<bool> ExistsByGroupNumberAsyncExceptId(int groupNumber, Guid id, string description);
    }

    /// <summary>
    /// Implementación genérica del repositorio base con operaciones CRUD estándar.
    /// Proporciona funcionalidad común reutilizable para todas las entidades.
    /// </summary>
    /// <typeparam name="T">Tipo de entidad que implementa IEntity.</typeparam>
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IEntity
    {
        /// <summary>
        /// Contexto de Entity Framework para acceso a la base de datos.
        /// </summary>
        protected readonly AppDbContext _context;

        /// <summary>
        /// Conjunto de datos para la entidad T.
        /// </summary>
        protected readonly DbSet<T> _dbSet;

        /// <summary>
        /// Constructor que inicializa el repositorio con el contexto.
        /// </summary>
        /// <param name="context">Contexto de base de datos.</param>
        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        /// <summary>
        /// Obtiene una entidad por ID.
        /// </summary>
        public virtual async Task<T?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

        /// <summary>
        /// Obtiene todas las entidades.
        /// </summary>
        public virtual async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        /// <summary>
        /// Agrega una entidad y guarda cambios.
        /// </summary>
        public virtual async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Actualiza los valores de una entidad existente en la base de datos, comparando por ID.
        /// Lanza Exception si la entidad no se encuentra.
        /// </summary>
        /// <param name="entity">Entidad con los valores actualizados.</param>
        public virtual async Task UpdateAsync(T entity)
        {
            var existing = await _dbSet.FindAsync(entity.Id);

            if (existing == null) throw new Exception("Entidad no encontrada.");

            _context.Entry(existing).CurrentValues.SetValues(entity);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Elimina una entidad por su ID.
        /// Si hay restricciones de clave foránea, lanza InvalidOperationException con mensaje "FK_CONSTRAINT".
        /// </summary>
        /// <param name="id">ID de la entidad a eliminar.</param>
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

        /// <summary>
        /// Verifica si existe una entidad con un nombre único (sin distinguir mayúsculas/minúsculas).
        /// Solo aplica a entidades que implementan IUniqueName. Lanza InvalidCastException si no implementa.
        /// </summary>
        /// <param name="name">Nombre a verificar.</param>
        /// <returns>Verdadero si el nombre ya existe en otra entidad, falso en caso contrario.</returns>
        public virtual async Task<bool> ExistsByNameAsync(string name)
        {
            return await _dbSet.Cast<IUniqueName>().AnyAsync(e => e.Nombre.ToLower() == name.ToLower());
        }

        /// <summary>
        /// Verifica si existe una entidad con un nombre único, excluyendo una entidad específica por ID.
        /// Solo para entidades que implementan IUniqueName.
        /// </summary>
        /// <param name="name">Nombre a verificar.</param>
        /// <param name="id">ID de la entidad a excluir de la verificación.</param>
        /// <returns>Verdadero si el nombre existe en otra entidad, falso en caso contrario.</returns>
        public virtual async Task<bool> ExistsByNameAsyncExceptId(string name, Guid id)
        {
            return await _dbSet.Where(e => ((IUniqueName)e).Nombre.ToLower() == name.ToLower()).AnyAsync(e => e.Id != id);
        }

        /// <summary>
        /// Verifica si existe un número de grupo único o una descripción duplicada (sin distinguir mayúsculas/minúsculas).
        /// Solo aplica a entidades que implementan IUniqueNumber. Lanza InvalidCastException si no implementa.
        /// </summary>
        /// <param name="groupNumber">Número del grupo a verificar.</param>
        /// <param name="description">Descripción del grupo a verificar.</param>
        /// <returns>Verdadero si el número o descripción ya existen, falso en caso contrario.</returns>
        public virtual async Task<bool> ExistsByGroupNumberAsync(int groupNumber, string description)
        {
            return await _dbSet.Cast<IUniqueNumber>().AnyAsync(e => e.NumeroGrupo == groupNumber || e.DescripcionGrupo.ToLower() == description.ToLower());
        }

        /// <summary>
        /// Verifica si existe un número de grupo único o descripción, excluyendo una entidad específica por ID.
        /// Solo para entidades que implementan IUniqueNumber.
        /// </summary>
        /// <param name="groupNumber">Número del grupo a verificar.</param>
        /// <param name="id">ID de la entidad a excluir de la verificación.</param>
        /// <param name="description">Descripción del grupo a verificar.</param>
        /// <returns>Verdadero si el número o descripción existen en otra entidad, falso en caso contrario.</returns>
        public virtual async Task<bool> ExistsByGroupNumberAsyncExceptId(int groupNumber, Guid id, string description)
        {
            return await _dbSet.Where(e => ((IUniqueNumber)e).NumeroGrupo == groupNumber || ((IUniqueNumber)e).DescripcionGrupo.ToLower() == description.ToLower()).AnyAsync(e => e.Id != id);
        }
    }

}
