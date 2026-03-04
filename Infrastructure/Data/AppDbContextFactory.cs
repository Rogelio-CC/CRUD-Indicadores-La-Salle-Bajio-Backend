using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace KPIBackend.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // 🔐 Usa un connection string DIRECTO para migraciones
            var connectionString = "Host=localhost;Database=kpi_db;Username=postgres;Password=${DB_PASSWORD}"; // En la parte del Password (${DB_PASSWORD}) se remplaza por la contraseña como tal (por el momento), ya que no identifica el valor que se encuentra en launchSettings.json.

            optionsBuilder.UseNpgsql(connectionString);

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
