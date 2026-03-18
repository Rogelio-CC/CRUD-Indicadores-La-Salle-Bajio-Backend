using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Npgsql;
using System;
using System.Linq;
using System.Collections.Generic;

namespace KPIBackend.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // Preferir variable de entorno (Neon suele exportar DATABASE_URL)
            var connectionUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

            var connectionString = ConvertPostgresUrlToConnectionStringinAppDbContextFactory(connectionUrl!);

            optionsBuilder.UseNpgsql(connectionString);

            return new AppDbContext(optionsBuilder.Options);
        }

        private static string ConvertPostgresUrlToConnectionStringinAppDbContextFactory(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) throw new ArgumentNullException(nameof(url));

            // Si ya viene en formato key=value lo devolvemos
            if (!url.StartsWith("postgres://", StringComparison.OrdinalIgnoreCase) &&
                !url.StartsWith("postgresql://", StringComparison.OrdinalIgnoreCase))
            {
                return url;
            }

            var uri = new Uri(url);
            var userInfo = uri.UserInfo.Split(':', 2);
            var user = Uri.UnescapeDataString(userInfo[0]);
            var pass = userInfo.Length > 1 ? Uri.UnescapeDataString(userInfo[1]) : string.Empty;
            var host = uri.Host;
            var port = uri.IsDefaultPort ? 5432 : uri.Port;
            var database = uri.AbsolutePath?.TrimStart('/') ?? string.Empty;

            // parse query into dictionary
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

            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = host,
                Port = port,
                Username = user,
                Password = pass,
                Database = database,
            };

            // Mapeo mínimo de opciones SSL/sslmode
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
                // por defecto activar SSL en entornos cloud
                builder.SslMode = SslMode.Require;
            }

            // Si Neon usa certificados autofirmados, esto evita fallos (decide según tu política de seguridad). Es probable que se pueda borrar y no afecte, ya que esta en desuso.
            builder.TrustServerCertificate = true;

            return builder.ToString();
        }
    }
}