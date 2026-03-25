using KPIBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace KPIBackend.Data
{
    /// <summary>
    /// Contexto de base de datos principal para la aplicación KPI Backend de La Salle Bajío.
    /// Gestiona las entidades relacionadas con indicadores clave de rendimiento (KPIs),
    /// usuarios, facultades, carreras, actividades y otros componentes del sistema.
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Constructor del contexto de base de datos.
        /// </summary>
        /// <param name="options">Opciones de configuración para el DbContext.</param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        /// <summary>
        /// Conjunto de entidades para roles de usuario.
        /// </summary>
        public DbSet<Rol> roles { get; set; }
        /// <summary>
        /// Conjunto de entidades para facultades académicas.
        /// </summary>
        public DbSet<Facultad> facultades { get; set; }

        /// <summary>
        /// Conjunto de entidades para usuarios del sistema.
        /// </summary>
        public DbSet<Usuario> usuarios { get; set; }

        /// <summary>
        /// Conjunto de entidades para grupos de indicadores.
        /// </summary>
        public DbSet<GrupoIndicadores> grupo_indicadores { get; set; }

        /// <summary>
        /// Conjunto de entidades para periodos escolares.
        /// </summary>
        public DbSet<PeriodoEscolar> periodos_escolares { get; set; }

        /// <summary>
        /// Conjunto de entidades para directrices institucionales.
        /// </summary>
        public DbSet<Directriz> directrices { get; set; }

        /// <summary>
        /// Conjunto de entidades para indicadores de rendimiento.
        /// </summary>
        public DbSet<Indicador> indicadores { get; set; }

        /// <summary>
        /// Conjunto de entidades para estrategias de mejora.
        /// </summary>
        public DbSet<Estrategia> estrategias { get; set; }

        /// <summary>
        /// Conjunto de entidades para actividades relacionadas con indicadores.
        /// </summary>
        public DbSet<Actividad> actividades { get; set; }

        /// <summary>
        /// Conjunto de entidades para comentarios en el sistema.
        /// </summary>
        public DbSet<Comentario> comentarios { get; set; }

        /// <summary>
        /// Conjunto de entidades para carreras académicas.
        /// </summary>
        public DbSet<Carrera> carreras { get; set; }

        /// <summary>
        /// Conjunto de entidades para evidencias de indicadores.
        /// </summary>
        public DbSet<Evidencia> evidencias { get; set; }

        /// <summary>
        /// Conjunto de entidades para archivo de politicas de una facultad.
        /// </summary>
        public DbSet<ArchivoPoliticas> archivoPoliticas { get; set; }

        /// <summary>
        /// Configura el modelo de datos al crear el contexto.
        /// Establece valores predeterminados para IDs usando UUIDs generados aleatoriamente,
        /// configura relaciones entre entidades y define comportamientos de eliminación.
        /// </summary>
        /// <param name="modelBuilder">Constructor del modelo para configurar entidades.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configura valores predeterminados para IDs de entidades usando UUIDs aleatorios.
            modelBuilder.Entity<Actividad>()
            .Property(r => r.Id)
            .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<Carrera>()
            .Property(r => r.Id)
            .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<Comentario>()
            .Property(r => r.Id)
            .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<Comentario>()
           .Property(r => r.IdObjetivo)
           .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<Directriz>()
            .Property(r => r.Id)
            .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<Estrategia>()
            .Property(r => r.Id)
            .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<Evidencia>()
            .Property(r => r.Id)
            .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<Facultad>()
            .Property(r => r.Id)
            .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<GrupoIndicadores>()
            .Property(r => r.Id)
            .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<Indicador>()
            .Property(r => r.Id)
            .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<PeriodoEscolar>()
            .Property(r => r.Id)
            .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<Rol>()
            .Property(r => r.Id)
            .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<Usuario>()
            .Property(r => r.Id)
            .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<ArchivoPoliticas>()
           .Property(r => r.Id)
           .HasDefaultValueSql("gen_random_uuid()");

            // Configura la relación entre Evidencia e Indicador con eliminación en cascada.
            modelBuilder.Entity<Evidencia>()
            .HasOne(e => e.Indicador)
            .WithMany(i => i.Evidencias)
            .HasForeignKey(e => e.IndicadorId)
    .OnDelete(DeleteBehavior.Cascade);

            // Configura la relación entre ArchivoPoliticas y Facultad con eliminación en cascada
            modelBuilder.Entity<ArchivoPoliticas>()
            .HasOne(e => e.Facultad)
            .WithMany(i => i.ArchivoPoliticas)
            .HasForeignKey(e => e.FacultadId)
    .OnDelete(DeleteBehavior.Cascade);

            // Establece comportamiento de eliminación restrictiva para todas las claves foráneas.
            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
{
                // ❗ NO tocar la relación Evidencia → Indicador
                if (foreignKey.DeclaringEntityType.ClrType == typeof(Evidencia))
                    continue;

                // ❗ NO tocar la relación ArchivoPoliticas → Facultad
                if (foreignKey.DeclaringEntityType.ClrType == typeof(ArchivoPoliticas))
                    continue;

                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }

            // Se añaden datos iniciales para la tablas como Rol, Facultad, Carrera, Usuario y EventoCalendario.
            // Esta misma estructura de código se puede usar para añadir cualquier dato inicial
            // requerido sin necesidad de insertarlo manualmente en la base de datos.
            modelBuilder.Entity<Rol>().HasData(
                new Rol
                {
                    Id = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d473"),
                    Nombre = "Administrador",
                },
                new Rol
                {
                    Id = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d481"),
                    Nombre = "Maestro",
                }
            );

            modelBuilder.Entity<Facultad>().HasData(
                new Facultad
                {
                    Id = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d476"),
                    Nombre = "Facultad de Tecnología",
                    Mision = "Buscar la mejora tecnológica.",
                    Vision = "En 2030, ser una facultad lider en avances tecnológicos.",
                    Slogan = "Un mundo mejor con tecnología.",
                    FechaEmision = new DateTime(2026, 3, 17).ToUniversalTime(),

                }
            );

            modelBuilder.Entity<Carrera>().HasData(
                new Carrera
                {
                    Id = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d488"),
                    NombreCarrera = "Ingeniería de Software y Sistemas Computacionales.",
                    FacultadId = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d476"),
                }
            );

            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    Id = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d490"),
                    NombreUsuario = "Rogelio Ceballos Castillo",
                    CorreoInstitucional = "rcc74823@lasallebajio.edu.mx",
                    TipoUsuario = "Administrador",
                    RolId = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d473"),
                    FacultadId = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d476"),
                    CarreraId = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d488")
                }
            );
        }

        /// <summary>
        /// Configura la inserción de fechas automáticamente cuando se crea o se edita una entidad.
        /// Esto aplica a entidades como Facultad, Indicador, Estrategia, Actividad y Comentario por manejar atributos
        /// como fecha de emisión o edición.
        /// </summary>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Facultad entity)
                {
                    if (entry.State == EntityState.Added)
                    {
                        entity.FechaEmision = DateTime.Now.Date.ToUniversalTime();
                    }
                }

                if (entry.Entity is Indicador i)
                {
                    if (entry.State == EntityState.Added)
                    {
                        i.FechaEmision = DateTime.Now.Date.ToUniversalTime();
                    }
                }

                if (entry.Entity is Estrategia e)
                {
                    if (entry.State == EntityState.Added)
                    {
                        e.FechaEmision = DateTime.Now.Date.ToUniversalTime();
                    }
                }

                if (entry.Entity is Actividad a)
                {
                    if (entry.State == EntityState.Added)
                    {
                        a.FechaEmision = DateTime.Now.Date.ToUniversalTime();
                    }
                }
                if (entry.Entity is Comentario c)
                {
                    if (entry.State == EntityState.Added)
                    {
                        c.FechaComentario = DateTime.Now.Date.ToUniversalTime();
                    }
                }

            }

            return await base.SaveChangesAsync(cancellationToken);
        }


    }
}
