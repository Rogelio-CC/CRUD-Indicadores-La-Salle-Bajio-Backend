using KPIBackend.Data;
using KPIBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace KPIBackend.Repositories
{
    public interface ICarreraRepository : IBaseRepository<Carrera>
    {
        Task<IEnumerable<Carrera>> GetByFacultadAsync(Guid facultadId);
    }

    public class CarreraRepository : BaseRepository<Carrera>, ICarreraRepository
    {
        public CarreraRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Carrera>> GetByFacultadAsync(Guid facultadId) =>
            await _dbSet.Where(c => c.FacultadId == facultadId).ToListAsync();
    }
}
