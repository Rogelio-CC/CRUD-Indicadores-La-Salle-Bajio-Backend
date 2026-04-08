using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace KPIBackend.Data
{
    /// <summary>
    /// Factory para crear instancias de AppDbContext en tiempo de diseño.
    /// Implementa IDesignTimeDbContextFactory para permitir la ejecución de migraciones de Entity Framework.
    /// </summary>
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        /// <summary>
        /// Crea una instancia configurada del AppDbContext.
        /// Utiliza una cadena de conexión directa a la base de datos Sql server para migraciones.
        /// </summary>
        /// <param name="args">Argumentos de línea de comandos (no utilizados actualmente).</param>
        /// <returns>Instancia configurada de AppDbContext.</returns>
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // Cadena de conexión para acceso a la base de datos SQL server durante migraciones
            // Primera nota: La variable de entorno (DATABASE_CONNECTION_STRING) debe reemplazarse con el valor real en archivos como
            // launchSetting.json, appsettings.json o archivo .env (actualmente se manejan las variables de entorno con este último)
            // para que se reconozca el valor sin problema.
            // Segunda nota: AppDbContextFactory no lee correctamente el valor cuando se pone como variable de entorno en el archivo .env, que es donde se manejan actualmente las variables de entorno.
            // Por ende, únicamente se debe de poner el valor directamente, remplazando "<poner la cadena de valor de sql server aquí>"
            // por el valor real, cuando se quiera hacer migraciones. De lo contrario, dejar de este modo el código
            // para que el valor no sea expuesto en el código. Si se hacen migraciones y se desea subir cambios al repositorio,
            // se debe quitar el valor primero por lo antes mencionado.
            var connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING")
                ?? "<poner la cadena de valor de sql server aquí>";

            optionsBuilder.UseSqlServer(connectionString);

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}