using KPIBackend.Application.DTOs.Comentario;
using KPIBackend.Application.DTOs.ListaCombos;
using KPIBackend.Data;
using KPIBackend.Models;
using KPIBackend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace KPIBackend.Controllers
{
    /// <summary>
    /// Controlador encargado de gestionar los comentarios del sistema.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/comentarios")]
    public class ComentariosController : BaseController<Comentario>
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Constructor del controlador de actividad.
        /// </summary>
        /// <param name="repository">Repositorio del comentario.</param>
        /// <param name="context">Configuración para uso de la base de datos.</param>
        public ComentariosController(IComentarioRepository repository, AppDbContext context) : base(repository)
        {

            _context = context;
        }

        /// <summary>
        /// Obtiene todos los comentarios con información del usuario creador.
        /// </summary>
        /// <returns>Lista de comentarios.</returns>
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
                    CreadorId = d.CreadorId,
                    Creador = d.Creador.NombreUsuario,
                })
                .ToListAsync();

            return Ok(data);
        }

        /// <summary>
        /// Obtiene un comentario específico por su identificador.
        /// </summary>
        /// <param name="id">Identificador del comentario.</param>
        /// <returns>Comentario encontrado.</returns>
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
                CreadorId = d.CreadorId,
                Creador = d.Creador.NombreUsuario,

            })
            .FirstOrDefaultAsync();

            if (dto == null)
                return NotFound("El comentario no existe.");

            return Ok(dto);
        }

        /// <summary>
        /// Crea un nuevo comentario en el sistema.
        /// </summary>
        /// <param name="dto">Datos del comentario.</param>
        /// <returns>Resultado de la operación.</returns>
        [HttpPost("dto")]
        public async Task<IActionResult> Create(ComentarioCreateUpdateDto dto)
        {
            // Validaciones explícitas para errores de estado HTTP.
            if (dto == null)
                return BadRequest("Los datos del comentario no pueden estar vacíos.");

            if (dto.Contenido.Length <= 3 || dto.Contenido.Length > 1000)
                return BadRequest("El contenido del comentario debe tener al menos 3 caracteres y no puede exceder los 1000 caracteres.");

            if (dto.TipoObjetivo != "Directriz" && dto.TipoObjetivo != "Actividad" && dto.TipoObjetivo != "Estrategia")
                return BadRequest("El tipo de objetivo debe ser 'Directriz', 'Actividad' o 'Estrategia'.");

            if (!await _context.usuarios.AnyAsync(f => f.Id == dto.CreadorId))
                return BadRequest("El ID del usuario especificado no existe.");

            // Limpieza de espacios en blanco para validar correctamente el contenido duplicado.
            var creacionContenidoComentario = limpiaEspacios(dto.Contenido);

            // Validación del contenido del comentario duplicado sin importar mayúsculas, minúsculas o espacios adicionales.
            var creacionContenidoComentarioParaValidar = limpiaEspacios(dto.Contenido).ToLower();

            if (await _context.comentarios.AnyAsync(d => d.Contenido.ToLower() == creacionContenidoComentarioParaValidar))
                return Conflict("Ya existe un comentario con ese contenido.");

            var comentario = new Comentario
            {
                Contenido = creacionContenidoComentario, // Se asigna el contenido limpio sin espacios adicionales.
                TipoObjetivo = dto.TipoObjetivo,
                CreadorId = dto.CreadorId,

            };

            _context.comentarios.Add(comentario);
            await _context.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// Actualiza un comentario existente.
        /// Solo el creador o un administrador puede modificarlo.
        /// </summary>
        /// <param name="id">Identificador del comentario.</param>
        /// <param name="dto">Datos actualizados.</param>
        /// <returns>Resultado de la operación.</returns>
        [HttpPut("dto/{id}")]
        public async Task<IActionResult> Update(Guid id, ComentarioCreateUpdateDto dto)
        {
            var comentario = await _context.comentarios.FindAsync(id);

            // Validaciones explícitas para errores de estado HTTP.
            if (comentario == null)
                return NotFound("El comentario no existe.");

            var userIdFromComment = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var roleFromComment = User.FindFirst(ClaimTypes.Role)?.Value;

            if (roleFromComment != "Administrador" && comentario.CreadorId.ToString() != userIdFromComment)
                return Forbid();

            if (dto == null)
                return BadRequest("Los datos del comentario no pueden estar vacíos.");

            if (dto.Contenido.Length <= 3 || dto.Contenido.Length > 1000)
                return BadRequest("El contenido del comentario debe tener al menos 3 caracteres y no puede exceder los 1000 caracteres.");

            if (dto.TipoObjetivo != "Directriz" && dto.TipoObjetivo != "Actividad" && dto.TipoObjetivo != "Estrategia")
                return BadRequest("El tipo de objetivo debe ser 'Directriz', 'Actividad' o 'Estrategia'.");

            if (!await _context.usuarios.AnyAsync(f => f.Id == dto.CreadorId))
                return BadRequest("El ID del usuario especificado no existe.");

            var actualizacionContenidoComentario = limpiaEspacios(dto.Contenido);

            var actualizacionContenidoComentarioParaValidar = actualizacionContenidoComentario.ToLower();

            if (await _context.comentarios.AnyAsync(d => d.Contenido.ToLower() == actualizacionContenidoComentarioParaValidar && d.Id != id))
                return Conflict("Ya existe un comentario con ese contenido.");

            comentario.Contenido = actualizacionContenidoComentario;
            comentario.TipoObjetivo = dto.TipoObjetivo;
            comentario.CreadorId = dto.CreadorId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Obtiene una lista simplificada de comentarios para llenar combos en el frontend.
        /// </summary>
        /// <returns>Lista de carreras con Id (objetivo, no del comentario) y Nombre.</returns>
        [HttpGet("combo")]
        public async Task<IActionResult> GetCombo()
        {
            var objetivosComentarios = await _context.comentarios
                .Select(u => new ComentarioComboDTO
                {
                    Id = u.Id,
                    Nombre = u.Contenido
                })
                .ToListAsync();

            return Ok(objetivosComentarios);
        }

        /// <summary>
        /// Limpia los espacios en blanco para válidar correctamente el nombre duplicado.
        /// </summary>
        /// <returns>Contenido sin espacios en blanco innecesarios.</returns>
        private string limpiaEspacios(string texto) => Regex.Replace(texto.Trim(), @"\s+", " ");
    }
}
