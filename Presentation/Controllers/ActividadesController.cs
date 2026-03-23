using KPIBackend.Application.DTOs.Actividad;
using KPIBackend.Data;
using KPIBackend.Models;
using KPIBackend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace KPIBackend.Controllers
{
    /// <summary>
    /// Controlador encargado de gestionar las operaciones relacionadas
    /// con las actividades del sistema.
    /// </summary>
    /// <remarks>
    /// Permite consultar, crear y actualizar actividades asociadas
    /// a estrategias, periodos escolares y carreras.
    /// </remarks>
    [Authorize]
    [ApiController]
    [Route("api/actividades")]
    public class ActividadesController : BaseController<Actividad>
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Constructor del controlador de actividad.
        /// </summary>
        /// <param name="repository">Repositorio de la actividad.</param>
        /// <param name="context">Configuración para uso de la base de datos.</param>
        public ActividadesController(IActividadRepository repository, AppDbContext context) : base(repository) {

            _context = context;
        }

        /// <summary>
        /// Obtiene todas las actividades en formato DTO.
        /// </summary>
        /// <returns>Lista de actividades con información relacionada.</returns>
        [HttpGet("dto")]
        public async Task<ActionResult<List<ActividadDto>>> GetAllDto()
        {
            var data = await _context.actividades
                .Include(d => d.Estrategia)
                .Include(d => d.Creador)
                .Include(d => d.Periodo)
                .Include(d => d.Carrera)
                .Select(d => new ActividadDto
                {
                    Id = d.Id,
                    DescripcionActividad = d.DescripcionActividad,
                    CantidadLograda = d.CantidadLograda,
                    FechaEmision = d.FechaEmision,
                    FechaCumplimiento = d.FechaCumplimiento,
                    ActividadCumplida = d.ActividadCumplida,
                    EstrategiaId = d.EstrategiaId,
                    Estrategia = d.Estrategia.DescripcionEstrategia,
                    CreadorId = d.CreadorId,
                    Creador = d.Creador.NombreUsuario,
                    PeriodoId = d.PeriodoId,
                    Periodo = d.Periodo.Nombre,
                    CarreraId = d.CarreraId,
                    Carrera = d.Carrera.NombreCarrera,
                    ComentarioId = d.ComentarioId,
                    Comentario = d.Comentario!.Contenido,
                })
                .ToListAsync();

            return Ok(data);
        }

        /// <summary>
        /// Obtiene una actividad específica en formato DTO.
        /// </summary>
        /// <param name="id">Identificador de la actividad.</param>
        /// <returns>Actividad encontrada.</returns>
        [HttpGet("dto/{id}")]
        public async Task<IActionResult> GetDtoById(Guid id)
        {
            var dto = await _context.actividades
                .Include(d => d.Estrategia)
                .Include(d => d.Creador)
                .Include(d => d.Periodo)
                .Include(d => d.Carrera)
                .Where(d => d.Id == id)
                .Select(d => new ActividadDto
                {
                    Id = d.Id,
                    DescripcionActividad = d.DescripcionActividad,
                    CantidadLograda = d.CantidadLograda,
                    FechaEmision = d.FechaEmision,
                    FechaCumplimiento = d.FechaCumplimiento,
                    ActividadCumplida = d.ActividadCumplida,
                    EstrategiaId = d.EstrategiaId,
                    Estrategia = d.Estrategia.DescripcionEstrategia,
                    CreadorId = d.CreadorId,
                    Creador = d.Creador.NombreUsuario,
                    PeriodoId = d.PeriodoId,
                    Periodo = d.Periodo.Nombre,
                    CarreraId = d.CarreraId,
                    Carrera = d.Carrera.NombreCarrera,
                    ComentarioId = d.ComentarioId,
                    Comentario = d.Comentario!.Contenido,
                })
                .FirstOrDefaultAsync();

            if (dto == null)
                return NotFound("La actividad no existe");

            return Ok(dto);
        }

        /// <summary>
        /// Crea una nueva actividad.
        /// </summary>
        /// <param name="dto">Datos necesarios para crear la actividad.</param>
        /// <returns>Resultado de la operación.</returns>
        [HttpPost("dto")]
        public async Task<IActionResult> Create(ActividadCreateUpdateDto dto)
        {
            // Validaciones explícitas para errores de estado HTTP.
            if (string.IsNullOrWhiteSpace(dto.DescripcionActividad))
                return BadRequest("La descripción de la actividad es obligatoria");

            if(dto.DescripcionActividad.Length <= 3 || dto.DescripcionActividad.Length > 50)
                return BadRequest("La descripción de la actividad debe tener al menos un carácter y no puede exceder los 50 caracteres");
            
            if (dto.CantidadLograda < 0)
                return BadRequest("La cantidad lograda no puede ser negativa");
                
            if (dto.FechaCumplimiento < DateTimeOffset.Now.Date)
                return BadRequest("La fecha de cumplimiento no puede ser anterior a la fecha actual");

            if (dto.FechaCumplimiento > DateTimeOffset.Now.Date.AddYears(1))
                return BadRequest("La fecha de cumplimiento no puede ser tan lejana en el futuro");

            if (dto.EstrategiaId == Guid.Empty)
                return BadRequest("El ID de la estrategia es obligatorio");
            
            if (dto.CreadorId == Guid.Empty)
                return BadRequest("El ID del creador es obligatorio");

            if (dto.PeriodoId == Guid.Empty)
                return BadRequest("El ID del periodo es obligatorio");

            if (dto.CarreraId == Guid.Empty)
                return BadRequest("El ID de la carrera es obligatorio");

            if (!await _context.estrategias.AnyAsync(f => f.Id == dto.EstrategiaId))
                return BadRequest("El ID de la estrategia especificada no existe");

            if (!await _context.usuarios.AnyAsync(u => u.Id == dto.CreadorId))
                return BadRequest("El ID del creador especificado no existe");

            if (!await _context.periodos_escolares.AnyAsync(p => p.Id == dto.PeriodoId))
                return BadRequest("El ID del periodo especificado no existe");

            if (!await _context.carreras.AnyAsync(p => p.Id == dto.CarreraId))
                return BadRequest("El ID de la carrera especificada no existe");

            if (await _context.actividades.AnyAsync(e => e.DescripcionActividad.ToLower() == dto.DescripcionActividad.ToLower()))
                return Conflict("Ya existe una actividad con esa descripción");


            var actividad = new Actividad
            {
                DescripcionActividad = dto.DescripcionActividad,
                CantidadLograda = dto.CantidadLograda,
                FechaCumplimiento = dto.FechaCumplimiento?.ToUniversalTime(),
                ActividadCumplida = dto.ActividadCumplida,
                EstrategiaId = dto.EstrategiaId,
                CreadorId = dto.CreadorId,
                PeriodoId = dto.PeriodoId,
                CarreraId = dto.CarreraId,
                ComentarioId = dto.ComentarioId,
            };

            _context.actividades.Add(actividad);
            await _context.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// Actualiza una actividad existente.
        /// </summary>
        /// <param name="id">Identificador de la actividad.</param>
        /// <param name="dto">Datos actualizados.</param>
        /// <returns>Resultado de la actualización.</returns>
        [HttpPut("dto/{id}")]
        public async Task<IActionResult> Update(Guid id, ActividadCreateUpdateDto dto)
        {
            var actividad = await _context.actividades.FindAsync(id);

            // Validaciones explícitas para errores de estado HTTP.
            if (actividad == null)
                return NotFound("La actividad no existe");

            var userIdFromActivity = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var roleFromActivity = User.FindFirst(ClaimTypes.Role)?.Value;

            if (roleFromActivity != "Administrador" && actividad.CreadorId.ToString() != userIdFromActivity)
                return Forbid();

            if (string.IsNullOrWhiteSpace(dto.DescripcionActividad))
                return BadRequest("La descripción de la actividad es obligatoria");

            if(dto.DescripcionActividad.Length <= 3 || dto.DescripcionActividad.Length > 50)
                return BadRequest("La descripción de la actividad debe tener al menos un carácter y no puede exceder los 50 caracteres");
            
            if (dto.CantidadLograda < 0)
                return BadRequest("La cantidad lograda no puede ser negativa");

            if (dto.FechaCumplimiento < DateTimeOffset.Now.Date)
                return BadRequest("La fecha de cumplimiento no puede ser anterior a la fecha actual");

            if (dto.FechaCumplimiento > DateTimeOffset.Now.Date.AddYears(1))
                return BadRequest("La fecha de cumplimiento no puede ser tan lejana en el futuro");

            if (dto.EstrategiaId == Guid.Empty)
                return BadRequest("El ID de la estrategia es obligatorio");
            
            if (dto.CreadorId == Guid.Empty)
                return BadRequest("El ID del creador es obligatorio");

            if (dto.PeriodoId == Guid.Empty)
                return BadRequest("El ID del periodo es obligatorio");

            if (dto.CarreraId == Guid.Empty)
                return BadRequest("El ID de la carrera es obligatorio");

            if (!await _context.estrategias.AnyAsync(f => f.Id == dto.EstrategiaId))
                return BadRequest("El ID de la estrategia especificada no existe");

            if (!await _context.usuarios.AnyAsync(u => u.Id == dto.CreadorId))
                return BadRequest("El ID del creador especificado no existe");

            if (!await _context.periodos_escolares.AnyAsync(p => p.Id == dto.PeriodoId))
                return BadRequest("El ID del periodo especificado no existe");

            if (!await _context.carreras.AnyAsync(p => p.Id == dto.CarreraId))
                return BadRequest("El ID de la carrera especificada no existe");
            
            if (await _context.actividades.AnyAsync(e => e.DescripcionActividad.ToLower() == dto.DescripcionActividad.ToLower() && e.Id != id))
                return Conflict("Ya existe otra actividad con esa descripción");

            actividad.DescripcionActividad = dto.DescripcionActividad;
            actividad.CantidadLograda = dto.CantidadLograda;
            actividad.FechaCumplimiento = dto.FechaCumplimiento?.ToUniversalTime();
            actividad.ActividadCumplida = dto.ActividadCumplida;
            actividad.EstrategiaId = dto.EstrategiaId;
            actividad.CreadorId = dto.CreadorId;
            actividad.PeriodoId = dto.PeriodoId;
            actividad.CarreraId = dto.CarreraId;
            actividad.ComentarioId = dto.ComentarioId;

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

