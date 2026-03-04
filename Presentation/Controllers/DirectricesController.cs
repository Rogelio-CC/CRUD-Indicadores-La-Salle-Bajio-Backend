using KPIBackend.Application.DTOs.Directriz;
using KPIBackend.Application.DTOs.ListaCombos;
using KPIBackend.Data;
using KPIBackend.Models;
using KPIBackend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaAutenticador2.Shared.DTOS.ListaCombos;
using System.Security.Claims;

namespace KPIBackend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/directrices")]
    public class DirectricesController : BaseController<Directriz>
    {
        private readonly AppDbContext _context;

        public DirectricesController(IDirectrizRepository repository, AppDbContext context) : base(repository) {

            _context = context;
        }

        [HttpGet("dto")]
        public async Task<ActionResult<List<DirectrizDto>>> GetAllDto()
        {
            var data = await _context.directrices
                .Include(d => d.Facultad)
                .Include(d => d.Creador)
                .Include(d => d.Periodo)
                .Select(d => new DirectrizDto
                {
                    Id = d.Id,
                    Descripcion = d.Descripcion,
                    FacultadId = d.FacultadId,
                    Facultad = d.Facultad.Nombre,
                    CreadorId = d.CreadorId,
                    Creador = d.Creador.NombreUsuario,
                    PeriodoId = d.PeriodoId,
                    Periodo = d.Periodo.Nombre
                })
                .ToListAsync();

            return Ok(data);
        }

        [HttpGet("dto/{id}")]
        public async Task<IActionResult> GetDtoById(Guid id)
        {
            var dto = await _context.directrices
            .Include(d => d.Facultad)
            .Include(d => d.Creador)
            .Include(d => d.Periodo)
            .Where(d => d.Id == id)

            .Select(d => new DirectrizDto
            {
                Id = d.Id,
                Descripcion = d.Descripcion,
                FacultadId = d.FacultadId,
                Facultad = d.Facultad.Nombre,
                CreadorId = d.CreadorId,
                Creador = d.Creador.NombreUsuario,
                PeriodoId = d.PeriodoId,
                Periodo = d.Periodo.Nombre
            })
            .FirstOrDefaultAsync();

            if (dto == null)
                return NotFound("La directriz no existe");

            return Ok(dto);
        }


        [HttpPost("dto")]
        public async Task<IActionResult> Create(DirectrizCreateUpdateDto dto)
        {
            // Validaciones explícitas (muy importante)
            if (dto == null)
                return BadRequest("Los datos de la directriz no pueden estar vacíos");

             if(dto.Descripcion.Length <= 3 || dto.Descripcion.Length > 50)
                return BadRequest("La descripción de la directriz debe tener al menos un carácter y no puede exceder los 50 caracteres");
                
            if (!await _context.facultades.AnyAsync(f => f.Id == dto.FacultadId))
                return BadRequest("El ID de la facultad especificada no existe");

            if (!await _context.usuarios.AnyAsync(u => u.Id == dto.CreadorId))
                return BadRequest("El ID del creador especificado no existe");

            if (!await _context.periodos_escolares.AnyAsync(p => p.Id == dto.PeriodoId))
                return BadRequest("El ID del periodo especificado no existe");
            
            if (await _context.directrices.AnyAsync(d => d.Descripcion.ToLower() == dto.Descripcion.ToLower()))
                return Conflict("Ya existe una directriz con esa descripción");

            var directriz = new Directriz
            {
                Descripcion = dto.Descripcion,
                FacultadId = dto.FacultadId,
                CreadorId = dto.CreadorId,
                PeriodoId = dto.PeriodoId
            };

            _context.directrices.Add(directriz);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("dto/{id}")]
        public async Task<IActionResult> Update(Guid id, DirectrizCreateUpdateDto dto)
        {
            var directriz = await _context.directrices.FindAsync(id);

            // Validaciones explícitas (muy importante)
            if (directriz == null)
                return NotFound("La directriz no existe");

            var userIdFromGuideline = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var roleFromGuideline = User.FindFirst(ClaimTypes.Role)?.Value;

            if (roleFromGuideline != "Administrador" && directriz.CreadorId.ToString() != userIdFromGuideline)
                return Forbid();

            if (dto == null)
                return BadRequest("Los datos de la directriz no pueden estar vacíos");

            if(dto.Descripcion.Length <= 3 || dto.Descripcion.Length > 50)
                return BadRequest("La descripción de la directriz debe tener al menos un carácter y no puede exceder los 50 caracteres");

            if (!await _context.facultades.AnyAsync(f => f.Id == dto.FacultadId))
                return BadRequest("El ID de la facultad especificada no existe");

            if (!await _context.usuarios.AnyAsync(u => u.Id == dto.CreadorId))
                return BadRequest("El ID del creador especificado no existe");

            if (!await _context.periodos_escolares.AnyAsync(p => p.Id == dto.PeriodoId))
                return BadRequest("El ID del periodo especificado no existe");

            if (await _context.directrices.AnyAsync(d => d.Descripcion.ToLower() == dto.Descripcion.ToLower() && d.Id != id))
                return Conflict("Ya existe otra directriz con esa descripción");

            // Actualización
            directriz.Descripcion = dto.Descripcion;
            directriz.FacultadId = dto.FacultadId;
            directriz.CreadorId = dto.CreadorId;
            directriz.PeriodoId = dto.PeriodoId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("combo")]
        public async Task<IActionResult> GetCombo()
        {
            var directrices = await _context.directrices
                .Select(u => new DirectrizComboDTO
                {
                    Id = u.Id,
                    Nombre = u.Descripcion
                })
                .ToListAsync();

            return Ok(directrices);
        }




    }


}
