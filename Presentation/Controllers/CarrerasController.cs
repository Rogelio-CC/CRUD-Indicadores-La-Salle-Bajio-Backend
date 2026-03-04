using KPIBackend.Application.DTOs.Carrera;
using KPIBackend.Application.DTOs.Directriz;
using KPIBackend.Application.DTOs.ListaCombos;
using KPIBackend.Data;
using KPIBackend.Models;
using KPIBackend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KPIBackend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/carreras")]
    public class CarrerasController : BaseController<Carrera>
    {
        private readonly AppDbContext _context;

        public CarrerasController(ICarreraRepository repository, AppDbContext context) : base(repository) {
        
            _context = context;
        }

        [HttpGet("dto")]
        public async Task<ActionResult<List<CarreraDto>>> GetAllDto()
        {
            var data = await _context.carreras
                .Include(d => d.Facultad)
                .Select(d => new CarreraDto
                {
                    Id = d.Id,
                    NombreCarrera = d.NombreCarrera,
                    FacultadId = d.FacultadId,
                    Facultad = d.Facultad.Nombre
                })
                .ToListAsync();

            return Ok(data);
        }

        [HttpGet("dto/{id}")]
        public async Task<IActionResult> GetDtoById(Guid id)
        {
            var dto = await _context.carreras
            .Include(d => d.Facultad)
            .Where(d => d.Id == id)

            .Select(d => new CarreraDto
            {
                Id = d.Id,
                NombreCarrera = d.NombreCarrera,
                FacultadId = d.FacultadId,
                Facultad = d.Facultad.Nombre
            })
            .FirstOrDefaultAsync();

            if (dto == null)
                return NotFound("La carrera no existe");

            return Ok(dto);
        }


        [HttpPost("dto")]
        public async Task<IActionResult> Create(CarreraCreateUpdateDto dto)
        {
            // Validaciones explícitas (muy importante)
            if (dto == null)
                return BadRequest("Los datos de la carrera no pueden estar vacíos");

            if(dto.NombreCarrera.Length <= 3 || dto.NombreCarrera.Length > 50)
                return BadRequest("El nombre de la carrera debe tener al menos un carácter y no puede exceder los 50 caracteres");

            if (!await _context.facultades.AnyAsync(f => f.Id == dto.FacultadId))
                return BadRequest("El ID de la facultad especificada no existe");

            if (await _context.carreras.AnyAsync(c => c.NombreCarrera.ToLower() == dto.NombreCarrera.ToLower()))
                return Conflict("Ya existe una carrera con ese nombre");

            var carrera = new Carrera
            {
                NombreCarrera = dto.NombreCarrera,
                FacultadId = dto.FacultadId,
            };

            _context.carreras.Add(carrera);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("dto/{id}")]
        public async Task<IActionResult> Update(Guid id, CarreraCreateUpdateDto dto)
        {
            var carrera = await _context.carreras.FindAsync(id);

            // Validaciones explícitas (muy importante)
            if (carrera == null)
                return NotFound("La carrera no existe");

            if (dto == null)
                return BadRequest("Los datos de la carrera no pueden estar vacíos");

            if(dto.NombreCarrera.Length <= 3 || dto.NombreCarrera.Length > 50)
                return BadRequest("El nombre de la carrera debe tener al menos un carácter y no puede exceder los 50 caracteres");

            if (!await _context.facultades.AnyAsync(f => f.Id == dto.FacultadId))
                return BadRequest("El ID de la facultad especificada no existe");

             if (await _context.carreras.AnyAsync(c => c.NombreCarrera.ToLower() == dto.NombreCarrera.ToLower() && c.Id != id))
                return Conflict("Ya existe otra carrera con ese nombre");

            // Actualización
            carrera.NombreCarrera = dto.NombreCarrera;
            carrera.FacultadId = dto.FacultadId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("combo")]
        public async Task<IActionResult> GetCombo()
        {
            var carreras = await _context.carreras
                .Select(u => new CarreraComboDTO
                {
                    Id = u.Id,
                    Nombre = u.NombreCarrera
                })
                .ToListAsync();
                
            return Ok(carreras);
        }
    }
}
