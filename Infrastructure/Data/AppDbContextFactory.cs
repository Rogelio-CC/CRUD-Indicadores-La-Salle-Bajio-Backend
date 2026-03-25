using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Npgsql;

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
        /// Utiliza una cadena de conexión directa a la base de datos PostgreSQL para migraciones.
        /// </summary>
        /// <param name="args">Argumentos de línea de comandos (no utilizados actualmente).</param>
        /// <returns>Instancia configurada de AppDbContext.</returns>
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // Cadena de conexión para acceso a la base de datos PostgreSQL durante migraciones
            // Nota: La variable de entorno (DATABASE_URL) debe reemplazarse con el valor real en archivos como
            // launchSetting.json, appsettings.json o archivo .env (actualmente se manejan las variables de entorno con este último)
            // para que Neon reconozca el valor sin problema.
            // Nota: AppDbContextFactory no lee correctamente el valor cuando se pone como variable de entorno en el archivo .env, que es donde se manejan actualmente las variables de entorno.
            // Por ende, únicamente se debe de poner el valor directamente, remplazando "<poner la cadena de valor de Neon aquí>"
            // por el valor real, cuando se quiera hacer migraciones. De lo contrario, dejar de este modo el código
            // para que el valor no sea expuesto en el código. Si se hacen migraciones y se desea subir cambios al repositorio,
            // se debe quitar el valor primero por lo antes mencionado.
            var connectionUrl = Environment.GetEnvironmentVariable("DATABASE_URL")
                ?? "postgresql://neondb_owner:npg_lTp7da1BgyHP@ep-rough-cherry-a8cxocab-pooler.eastus2.azure.neon.tech/kpi_db_indicadores_salle?sslmode=require&channel_binding=require";

            // Para que la cadena de Neon se reconozca correctamente en este proyecto, se necesita convertir la cadena de tal manera que la configuración
            // local de PostreSQL permite ese valor y poder hacer migraciones en Neon.
            var connectionString = ConvertPostgresUrlToConnectionStringinAppDbContextFactory(connectionUrl);

            optionsBuilder.UseNpgsql(connectionString);

            return new AppDbContext(optionsBuilder.Options);
        }

        // Método para convertir la cadena de Neon en un valor Key de PostreSQL.
        private static string ConvertPostgresUrlToConnectionStringinAppDbContextFactory(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) throw new ArgumentNullException(nameof(url));

            // Si ya viene en formato key=value lo devolvemos
            if (!url.StartsWith("postgres://", StringComparison.OrdinalIgnoreCase) &&
                !url.StartsWith("postgresql://", StringComparison.OrdinalIgnoreCase))
            {
                return url;
            }

            // Esta parte de código ayuda a separar los valores juntados en la cadena de Neon en valores separados para implementarlos
            // en la construcción de la conexión de PostreSQL más adelante.
            var uri = new Uri(url);
            var userInfo = uri.UserInfo.Split(':', 2);
            var user = Uri.UnescapeDataString(userInfo[0]);
            var pass = userInfo.Length > 1 ? Uri.UnescapeDataString(userInfo[1]) : string.Empty;
            var host = uri.Host;
            var port = uri.IsDefaultPort ? 5432 : uri.Port;
            var database = uri.AbsolutePath?.TrimStart('/') ?? string.Empty;

            var queryDict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            var rawQuery = uri.Query?.TrimStart('?') ?? string.Empty;
            if (!string.IsNullOrEmpty(rawQuery))
            {
                foreach (var part in rawQuery.Split('&', StringSplitOptions.RemoveEmptyEntries))
                {
                    var kv = part.Split('=', 2);
                    var k = Uri.UnescapeDataString(kv[0]);
                    var v = kv.Length > 1 ? Uri.UnescapeDataString(kv[1]) : string.Empty;
                    queryDict[k] = v;
                }
            }

            // Toma los valores separados de la cadena de Neon que lee la configuración de PostgreSQL para que funcione
            // el vinculo entre Neon y las migraciones.
            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = host,
                Port = port,
                Username = user,
                Password = pass,
                Database = database,
            };

            // Mapeo mínimo de opciones SSL/sslmode.
            if (queryDict.TryGetValue("sslmode", out var sslMode))
            {
                if (sslMode.Equals("disable", StringComparison.OrdinalIgnoreCase))
                    builder.SslMode = SslMode.Disable;
                else if (sslMode.Equals("require", StringComparison.OrdinalIgnoreCase) || sslMode.Equals("prefer", StringComparison.OrdinalIgnoreCase))
                    builder.SslMode = SslMode.Require;
                else if (sslMode.Equals("verify-ca", StringComparison.OrdinalIgnoreCase) || sslMode.Equals("verify-full", StringComparison.OrdinalIgnoreCase))
                    builder.SslMode = SslMode.VerifyFull;
            }
            else
            {
                // por defecto activar SSL en entornos cloud, como el entorno de Neon.
                builder.SslMode = SslMode.Require;
            }

            return builder.ToString();
        }
    }
}