using KPIBackend.Application.DTOs.Comentario;
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
    [Route("api/comentarios")]
    public class ComentariosController : BaseController<Comentario>
    {
        private readonly AppDbContext _context;
        public ComentariosController(IComentarioRepository repository, AppDbContext context) : base(repository) { 
        
            _context = context;
        }

        [HttpGet("dto")]
        public async Task<ActionResult<List<ComentarioDto>>> GetAllDto()
        {
            var data = await _context.comentarios
                .Include(d => d.Creador)
                .Select(d => new ComentarioDto
                {
                    Id = d.Id,
                    Contenido = d.Contenido,
                    FechaComentario = d.FechaComentario,
                    TipoObjetivo = d.TipoObjetivo,
                    IdObjetivo = d.IdObjetivo,
                    CreadorId = d.CreadorId,
                    Creador = d.Creador.NombreUsuario,
                })
                .ToListAsync();

            return Ok(data);
        }

        [HttpGet("dto/{id}")]
        public async Task<IActionResult> GetDtoById(Guid id)
        {
            var dto = await _context.comentarios
            .Include(d => d.Creador)
            .Where(d => d.Id == id)

            .Select(d => new ComentarioDto
            {
                Id = d.Id,
                Contenido = d.Contenido,
                FechaComentario = d.FechaComentario,
                TipoObjetivo = d.TipoObjetivo,
                IdObjetivo = d.IdObjetivo,
                CreadorId = d.CreadorId,
                Creador = d.Creador.NombreUsuario,

            })
            .FirstOrDefaultAsync();

            if (dto == null)
                return NotFound("El comentario no existe");

            return Ok(dto);
        }


        [HttpPost("dto")]
        public async Task<IActionResult> Create(ComentarioCreateUpdateDto dto)
        {
            // Validaciones explícitas (muy importante)
            if (dto == null)
                return BadRequest("Los datos del comentario no pueden estar vacíos");

            if(dto.Contenido.Length <= 3 || dto.Contenido.Length > 200)
                return BadRequest("El contenido del comentario debe tener al menos un carácter y no puede exceder los 200 caracteres");

            if (dto.TipoObjetivo != "Directriz" && dto.TipoObjetivo != "Actividad" && dto.TipoObjetivo != "Estrategia")
                return BadRequest("El tipo de objetivo debe ser 'Directriz', 'Actividad' o 'Estrategia'");

            if (!await _context.usuarios.AnyAsync(f => f.Id == dto.CreadorId))
                return BadRequest("El ID del usuario especificado no existe");

            if (await _context.comentarios.AnyAsync(d => d.Contenido.ToLower() == dto.Contenido.ToLower()))
                return Conflict("Ya existe un comentario con ese contenido");

            var comentario = new Comentario
            {
                Contenido = dto.Contenido,
                TipoObjetivo = dto.TipoObjetivo,
                CreadorId = dto.CreadorId,
                
            };

            _context.comentarios.Add(comentario);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("dto/{id}")]
        public async Task<IActionResult> Update(Guid id, ComentarioCreateUpdateDto dto)
        {
            var comentario = await _context.comentarios.FindAsync(id);

            // Validaciones explícitas (muy importante)
            if (comentario == null)
                return NotFound("El comentario no existe");

            var userIdFromComment = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var roleFromComment = User.FindFirst(ClaimTypes.Role)?.Value;

            if (roleFromComment != "Administrador" && comentario.CreadorId.ToString() != userIdFromComment)
                return Forbid();

            if (dto == null)
                return BadRequest("Los datos del comentario no pueden estar vacíos");

             if(dto.Contenido.Length <= 3 || dto.Contenido.Length > 200)
                return BadRequest("El contenido del comentario debe tener al menos un carácter y no puede exceder los 200 caracteres");

            if (dto.TipoObjetivo != "Directriz" && dto.TipoObjetivo != "Actividad" && dto.TipoObjetivo != "Estrategia")
                return BadRequest("El tipo de objetivo debe ser 'Directriz', 'Actividad' o 'Estrategia'");

            if (!await _context.usuarios.AnyAsync(f => f.Id == dto.CreadorId))
                return BadRequest("El ID del usuario especificado no existe");
            
            if (await _context.comentarios.AnyAsync(d => d.Contenido.ToLower() == dto.Contenido.ToLower() && d.Id != id))
                return Conflict("Ya existe un comentario con ese contenido");

            // Actualización
            comentario.Contenido  = dto.Contenido;
            comentario.TipoObjetivo  = dto.TipoObjetivo;
            comentario.CreadorId = dto.CreadorId;

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
