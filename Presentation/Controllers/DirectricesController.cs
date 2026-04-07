using KPIBackend.Application.DTOs.Directriz;
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
    /// Controlador encargado de gestionar las directrices del sistema.
    /// Permite crear, consultar y actualizar directrices académicas.
    /// </summary>
    /// <remarks>
    /// Las directrices están asociadas a una facultad, un periodo escolar y un usuario creador.
    /// </remarks>
    [Authorize]
    [ApiController]
    [Route("api/directrices")]
    public class DirectricesController : BaseController<Directriz>
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Constructor del controlador de actividad.
        /// </summary>
        /// <param name="repository">Repositorio de la directriz.</param>
        /// <param name="context">Configuración para uso de la base de datos.</param>
        public DirectricesController(IDirectrizRepository repository, AppDbContext context) : base(repository) {

            _context = context;
        }

        /// <summary>
        /// Obtiene todas las directrices del sistema en formato DTO.
        /// </summary>
        /// <returns>
        /// Lista de directrices con información de facultad, creador y periodo escolar.
        /// </returns>
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
                    Periodo = d.Periodo.Nombre,
                    ComentarioId = d.ComentarioId,
                    Comentario = d.Comentario!.Contenido,
                })
                .ToListAsync();

            return Ok(data);
        }

        /// <summary>
        /// Obtiene una directriz específica por su identificador.
        /// </summary>
        /// <param name="id">Identificador único de la directriz.</param>
        /// <returns>Directriz encontrada con información relacionada.</returns>
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
                Periodo = d.Periodo.Nombre,
                ComentarioId = d.ComentarioId,
                Comentario = d.Comentario!.Contenido,
            })
            .FirstOrDefaultAsync();

            if (dto == null)
                return NotFound("La directriz no existe.");

            return Ok(dto);
        }

        /// <summary>
        /// Crea una nueva directriz en el sistema.
        /// </summary>
        /// <param name="dto">
        /// Objeto que contiene la descripción, facultad, creador y periodo escolar de la directriz.
        /// </param>
        /// <returns>
        /// Resultado de la operación de creación.
        /// </returns>
        [HttpPost("dto")]
        public async Task<IActionResult> Create(DirectrizCreateUpdateDto dto)
        {
            // Validaciones explícitas para errores de estado HTTP.
            if (dto == null)
                return BadRequest("Los datos de la directriz no pueden estar vacíos.");

             if(dto.Descripcion.Length <= 3 || dto.Descripcion.Length > 500)
                return BadRequest("La descripción de la directriz debe tener al menos 3 caracteres y no puede exceder los 500 caracteres.");
                
            if (!await _context.facultades.AnyAsync(f => f.Id == dto.FacultadId))
                return BadRequest("El ID de la facultad especificada no existe.");

            if (!await _context.usuarios.AnyAsync(u => u.Id == dto.CreadorId))
                return BadRequest("El ID del creador especificado no existe.");

            if (!await _context.periodos_escolares.AnyAsync(p => p.Id == dto.PeriodoId))
                return BadRequest("El ID del periodo especificado no existe.");

            // Limpieza de espacios en blanco para validar correctamente la descripción duplicada.
            var creacionDescripcionDirectriz = limpiaEspacios(dto.Descripcion);

            // Validación de la descripción de la directriz duplicada sin importar mayúsculas, minúsculas o espacios adicionales.
            var creacionDescripcionDirectrizParaValidar = creacionDescripcionDirectriz.ToLower();

            if (await _context.directrices.AnyAsync(d => d.Descripcion.ToLower() == creacionDescripcionDirectrizParaValidar))
                return Conflict("Ya existe una directriz con esa descripción.");

            var directriz = new Directriz
            {
                Descripcion = creacionDescripcionDirectriz, // Se asigna la descripción limpia sin espacios adicionales.
                FacultadId = dto.FacultadId,
                CreadorId = dto.CreadorId,
                PeriodoId = dto.PeriodoId,
                ComentarioId = dto.ComentarioId,
            };

            _context.directrices.Add(directriz);
            await _context.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// Actualiza una directriz existente.
        /// </summary>
        /// <param name="id">Identificador de la directriz.</param>
        /// <param name="dto">Datos actualizados de la directriz.</param>
        /// <returns>
        /// Resultado de la operación de actualización.
        /// </returns>
        /// <remarks>
        /// Solo el creador de la directriz o un administrador pueden modificarla.
        /// </remarks>
        [HttpPut("dto/{id}")]
        public async Task<IActionResult> Update(Guid id, DirectrizCreateUpdateDto dto)
        {
            var directriz = await _context.directrices.FindAsync(id);

            // Validaciones explícitas para errores de estado HTTP.
            if (directriz == null)
                return NotFound("La directriz no existe.");

            var userIdFromGuideline = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var roleFromGuideline = User.FindFirst(ClaimTypes.Role)?.Value;

            if (roleFromGuideline != "Administrador" && directriz.CreadorId.ToString() != userIdFromGuideline)
                return Forbid();

            if (dto == null)
                return BadRequest("Los datos de la directriz no pueden estar vacíos.");

            if(dto.Descripcion.Length <= 3 || dto.Descripcion.Length > 500)
                return BadRequest("La descripción de la directriz debe tener al menos 3 caracteres y no puede exceder los 500 caracteres.");

            if (!await _context.facultades.AnyAsync(f => f.Id == dto.FacultadId))
                return BadRequest("El ID de la facultad especificada no existe.");

            if (!await _context.usuarios.AnyAsync(u => u.Id == dto.CreadorId))
                return BadRequest("El ID del creador especificado no existe.");

            if (!await _context.periodos_escolares.AnyAsync(p => p.Id == dto.PeriodoId))
                return BadRequest("El ID del periodo especificado no existe.");

            var actualizacionDescripcionDirectriz = limpiaEspacios(dto.Descripcion);

            var actualizacionDescripcionDirectrizParaValidar = actualizacionDescripcionDirectriz.ToLower();

            if (await _context.directrices.AnyAsync(d => d.Descripcion.ToLower() == actualizacionDescripcionDirectrizParaValidar && d.Id != id))
                return Conflict("Ya existe otra directriz con esa descripción.");

            directriz.Descripcion = actualizacionDescripcionDirectriz;
            directriz.FacultadId = dto.FacultadId;
            directriz.CreadorId = dto.CreadorId;
            directriz.PeriodoId = dto.PeriodoId;
            directriz.ComentarioId = dto.ComentarioId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Obtiene una lista simplificada de directrices para llenar combos en el frontend.
        /// </summary>
        /// <returns>
        /// Lista con identificador y nombre de la directriz.
        /// </returns>
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

        /// <summary>
        /// Limpia los espacios en blanco para válidar correctamente el nombre duplicado.
        /// </summary>
        /// <returns>Nombre/descripción sin espacios en blanco innecesarios.</returns>
        private string limpiaEspacios(string texto) => Regex.Replace(texto.Trim(), @"\s+", " ");
    }
}
