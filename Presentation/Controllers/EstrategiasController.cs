using KPIBackend.Application.DTOs.Estrategia;
using KPIBackend.Application.DTOs.ListaCombos;
using KPIBackend.Data;
using KPIBackend.Models;
using KPIBackend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace KPIBackend.Controllers
{
    [Authorize]
    [ApiController] 
    [Route("api/estrategias")]
    public class EstrategiasController : BaseController<Estrategia>
    {
        private readonly AppDbContext _context;

        public EstrategiasController(IEstrategiaRepository repository, AppDbContext context) : base(repository) {
        
            _context = context;
        }

        [HttpGet("dto")]
        public async Task<ActionResult<List<EstrategiaDto>>> GetAllDto()
        {
            var data = await _context.estrategias
                .Include(d => d.Indicador)
                .Include(d => d.Creador)
                .Include(d => d.Periodo)
                .Include(d => d.Carrera)
                .Select(d => new EstrategiaDto
                {
                    Id = d.Id,
                    DescripcionEstrategia = d.DescripcionEstrategia,
                    FechaEmision = d.FechaEmision,
                    IndicadorId = d.IndicadorId,
                    Indicador = d.Indicador.DescripcionIndicador,
                    CreadorId = d.CreadorId,
                    Creador = d.Creador.NombreUsuario,
                    PeriodoId = d.PeriodoId,
                    Periodo = d.Periodo.Nombre,
                    CarreraId = d.CarreraId,
                    Carrera = d.Carrera.NombreCarrera,
                })
                .ToListAsync();

            return Ok(data);
        }

        [HttpGet("dto/{id}")]
        public async Task<IActionResult> GetDtoById(Guid id)
        {
            var dto = await _context.estrategias
                .Include(d => d.Indicador)
                .Include(d => d.Creador)
                .Include(d => d.Periodo)
                .Include(d => d.Carrera)
                .Where(d => d.Id == id)
                .Select(d => new EstrategiaDto
                {
                    Id = d.Id,
                    DescripcionEstrategia = d.DescripcionEstrategia,
                    FechaEmision = d.FechaEmision,
                    IndicadorId = d.IndicadorId,
                    Indicador = d.Indicador.DescripcionIndicador,
                    CreadorId = d.CreadorId,
                    Creador = d.Creador.NombreUsuario,
                    PeriodoId = d.PeriodoId,
                    Periodo = d.Periodo.Nombre,
                    CarreraId = d.CarreraId,
                    Carrera = d.Carrera.NombreCarrera,
                })
                .FirstOrDefaultAsync();

            if (dto == null)
                return NotFound("La estrategia no existe");

            return Ok(dto);
        }


        [HttpPost("dto")]
        public async Task<IActionResult> Create(EstrategiaCreateUpdateDto dto)
        {
            // Validaciones explícitas (muy importante)
           if (dto == null)
                return BadRequest("Los datos de la estrategia no pueden estar vacíos");

            if(dto.DescripcionEstrategia.Length <= 3 || dto.DescripcionEstrategia.Length > 50)
                return BadRequest("La descripción de la estrategia debe tener al menos un carácter y no puede exceder los 50 caracteres");

            if (!await _context.indicadores.AnyAsync(f => f.Id == dto.IndicadorId))
                return BadRequest("El ID del indicador especificado no existe");

            if (!await _context.usuarios.AnyAsync(u => u.Id == dto.CreadorId))
                return BadRequest("El ID del creador especificado no existe");

            if (!await _context.periodos_escolares.AnyAsync(p => p.Id == dto.PeriodoId))
                return BadRequest("El ID del periodo especificado no existe");

            if (!await _context.carreras.AnyAsync(p => p.Id == dto.CarreraId))
                return BadRequest("El ID de la carrera especificada no existe");

            if (await _context.estrategias.AnyAsync(e => e.DescripcionEstrategia.ToLower() == dto.DescripcionEstrategia.ToLower()))
                return Conflict("Ya existe otra estrategia con esa descripción");

            var estrategia = new Estrategia
            {
                DescripcionEstrategia = dto.DescripcionEstrategia,
                IndicadorId = dto.IndicadorId,
                CreadorId = dto.CreadorId,
                PeriodoId = dto.PeriodoId,
                CarreraId = dto.CarreraId,
            };

            _context.estrategias.Add(estrategia);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("dto/{id}")]
        public async Task<IActionResult> Update(Guid id, EstrategiaCreateUpdateDto dto)
        {
            var estrategia = await _context.estrategias.FindAsync(id);

            // Validaciones explícitas (muy importante)
            if (estrategia == null)
                return NotFound("La estrategia no existe");

            var userIdFromStrategy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var roleFromStrategy = User.FindFirst(ClaimTypes.Role)?.Value;

            if (roleFromStrategy != "Administrador" && estrategia.CreadorId.ToString() != userIdFromStrategy)
                return Forbid();

            if (dto == null)
                return BadRequest("Los datos de la estrategia no pueden estar vacíos");

            if(dto.DescripcionEstrategia.Length <= 3 || dto.DescripcionEstrategia.Length > 50)
                return BadRequest("La descripción de la estrategia debe tener al menos un carácter y no puede exceder los 50 caracteres");

            if (!await _context.indicadores.AnyAsync(f => f.Id == dto.IndicadorId))
                return BadRequest("El ID del indicador especificado no existe");

            if (!await _context.usuarios.AnyAsync(u => u.Id == dto.CreadorId))
                return BadRequest("El ID del creador especificado no existe");

            if (!await _context.periodos_escolares.AnyAsync(p => p.Id == dto.PeriodoId))
                return BadRequest("El ID del periodo especificado no existe");

            if (!await _context.carreras.AnyAsync(p => p.Id == dto.CarreraId))
                return BadRequest("El ID de la carrera especificada no existe");

            if (await _context.estrategias.AnyAsync(e => e.DescripcionEstrategia.ToLower() == dto.DescripcionEstrategia.ToLower() && e.Id != id))
                return Conflict("Ya existe otra estrategia con esa descripción");

            // Actualización
            estrategia.DescripcionEstrategia = dto.DescripcionEstrategia;
            estrategia.IndicadorId = dto.IndicadorId;
            estrategia.CreadorId = dto.CreadorId;
            estrategia.PeriodoId = dto.PeriodoId;
            estrategia.CarreraId = dto.CarreraId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("combo")]
        public async Task<IActionResult> GetCombo()
        {
            var estrategias = await _context.estrategias
                .Select(u => new EstrategiaComboDTO
                {
                    Id = u.Id,
                    Nombre = u.DescripcionEstrategia
                })
                .ToListAsync();

            return Ok(estrategias);
        }
    }
}


