using Microsoft.EntityFrameworkCore;
using KPIBackend.Models;

namespace KPIBackend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Rol> roles { get; set; }
        public DbSet<Facultad> facultades { get; set; }
        public DbSet<Usuario> usuarios { get; set; }
        public DbSet<GrupoIndicadores> grupo_indicadores { get; set; }
        public DbSet<PeriodoEscolar> periodos_escolares { get; set; }
        public DbSet<Directriz> directrices { get; set; }
        public DbSet<Indicador> indicadores { get; set; }
        public DbSet<Estrategia> estrategias { get; set; }
        public DbSet<Actividad> actividades { get; set; }
        public DbSet<Comentario> comentarios { get; set; }
        public DbSet<Carrera> carreras { get; set; }
        public DbSet<Evidencia> evidencias { get; set; }
        public DbSet<EventoCalendario> eventosCalendario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Nuevo código agregado por mí
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

            modelBuilder.Entity<EventoCalendario>()
            .Property(r => r.Id)
            .HasDefaultValueSql("gen_random_uuid()");

            //modelBuilder.Entity<Usuario>()
            //    .Property(u => u.TipoUsuario)
            //    .HasConversion<string>();

            modelBuilder.Entity<Evidencia>()
            .HasOne(e => e.Indicador)
            .WithMany(i => i.Evidencias)
            .HasForeignKey(e => e.IndicadorId)
    .OnDelete(DeleteBehavior.Cascade);


            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }

            //Se añaden datos inciales para la tabla "eventosCalendario". 
            //Esta misma estrctura de código se puede usar para añadir cualquier dato inicial requerido sin necesidad de añadirlo mnualmente a la base de datos.
            modelBuilder.Entity<Rol>().HasData(
                new Rol
                {
                    Id = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d473"),
                    Nombre = "Administrador",
                    Permisos = "Puede modificar todas las tablas."
                },
                new Rol
                {
                    Id = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d481"),
                    Nombre = "Maestro",
                    Permisos = "Puede crear actividades."
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
                    PoliticaAsociada = "Política 1: uso ético de la tecnología.",
                    FechaEmision = new DateTime(2026, 3, 17).ToUniversalTime(),
                    FechaEdicion = null

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

            modelBuilder.Entity<EventoCalendario>().HasData(
                new EventoCalendario
                {
                    Id = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d475"),
                    Titulo = "Inicio del semestre escolar",
                    FechaInicio = new DateTime(2026, 2, 9).ToUniversalTime(),
                    FechaFin = new DateTime(2026, 6, 27).ToUniversalTime(),
                    TipoEvento = "Académico",
                    Color = "#BFBFBF"
                },
                new EventoCalendario
                {
                    Id = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d480"),
                    Titulo = "Semana sin actividad",
                    FechaInicio = new DateTime(2026, 3, 20).ToUniversalTime(),
                    FechaFin = new DateTime(2026, 3, 27).ToUniversalTime(),
                    TipoEvento = "Académico",
                    Color = null
                }
            );
        }

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

                    if (entry.State == EntityState.Modified)
                    {
                        entity.FechaEdicion = DateTime.Now.Date.ToUniversalTime();
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
