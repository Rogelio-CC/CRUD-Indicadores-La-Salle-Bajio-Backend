using KPIBackend.Application.DTOs.Carrera;
using KPIBackend.Application.DTOs.ListaCombos;
using KPIBackend.Data;
using KPIBackend.Models;
using KPIBackend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace KPIBackend.Controllers
{
    /// <summary>
    /// Controlador encargado de gestionar las carreras del sistema.
    /// </summary>
    /// 
    [Authorize]
    [ApiController]
    [Route("api/carreras")]
    public class CarrerasController : BaseController<Carrera>
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Constructor del controlador de actividad.
        /// </summary>
        /// <param name="repository">Repositorio de la carreras.</param>
        /// <param name="context">Configuración para uso de la base de datos.</param>
        public CarrerasController(ICarreraRepository repository, AppDbContext context) : base(repository)
        {

            _context = context;
        }

        /// <summary>
        /// Obtiene la lista de carreras en formato DTO.
        /// Incluye información de la facultad asociada.
        /// </summary>
        /// <returns>Lista de carreras.</returns>
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

        /// <summary>
        /// Obtiene una carrera específica en formato DTO.
        /// </summary>
        /// <param name="id">Identificador de la carrera.</param>
        /// <returns>Carrera encontrada.</returns>
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
                return NotFound("La carrera no existe.");

            return Ok(dto);
        }

        /// <summary>
        /// Crea una nueva carrera.
        /// </summary>
        /// <param name="dto">Datos de la carrera a crear.</param>
        /// <returns>Resultado de la operación.</returns>
        [HttpPost("dto")]
        public async Task<IActionResult> Create(CarreraCreateUpdateDto dto)
        {
            // Validaciones explícitas para errores de estado HTTP.
            if (dto == null)
                return BadRequest("Los datos de la carrera no pueden estar vacíos.");

            if (dto.NombreCarrera.Length <= 3 || dto.NombreCarrera.Length > 500)
                return BadRequest("El nombre de la carrera debe tener al menos 3 caracteres y no puede exceder los 500 caracteres.");

            if (!await _context.facultades.AnyAsync(f => f.Id == dto.FacultadId))
                return BadRequest("El ID de la facultad especificada no existe.");

            // Limpieza de espacios en blanco para validar correctamente el nombre duplicado.
            var creacionNombreCarrera = limpiaEspacios(dto.NombreCarrera);

            // Validación de nombre de carrera duplicado sin importar mayúsculas, minúsculas o espacios adicionales.
            var creacionNombreCarreraParaValidar = creacionNombreCarrera.ToLower();

            if (await _context.carreras.AnyAsync(c => c.NombreCarrera.ToLower() == creacionNombreCarreraParaValidar))
                return Conflict("Ya existe una carrera con ese nombre.");

            var carrera = new Carrera
            {
                NombreCarrera = creacionNombreCarrera, // Se asigna el nombre limpio sin espacios adicionales.
                FacultadId = dto.FacultadId,
            };

            _context.carreras.Add(carrera);
            await _context.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// Actualiza una carrera existente.
        /// </summary>
        /// <param name="id">Identificador de la carrera.</param>
        /// <param name="dto">Datos actualizados.</param>
        /// <returns>Resultado de la operación.</returns>   
        [HttpPut("dto/{id}")]
        public async Task<IActionResult> Update(Guid id, CarreraCreateUpdateDto dto)
        {
            var carrera = await _context.carreras.FindAsync(id);

            // Validaciones explícitas para errores de estado HTTP.
            if (carrera == null)
                return NotFound("La carrera no existe.");

            if (dto == null)
                return BadRequest("Los datos de la carrera no pueden estar vacíos.");

            if (dto.NombreCarrera.Length <= 3 || dto.NombreCarrera.Length > 500)
                return BadRequest("El nombre de la carrera debe tener al menos 3 caracteres y no puede exceder los 500 caracteres.");

            if (!await _context.facultades.AnyAsync(f => f.Id == dto.FacultadId))
                return BadRequest("El ID de la facultad especificada no existe.");

            var actualizacionNombreCarrera = limpiaEspacios(dto.NombreCarrera);

            var actualizacionNombreCarreraParaValidar = actualizacionNombreCarrera.ToLower();

            if (await _context.carreras.AnyAsync(c => c.NombreCarrera.ToLower() == actualizacionNombreCarreraParaValidar && c.Id != id))
                return Conflict("Ya existe otra carrera con ese nombre.");

            carrera.NombreCarrera = actualizacionNombreCarrera;
            carrera.FacultadId = dto.FacultadId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Obtiene una lista simplificada de carreras para llenar combos en el frontend.
        /// </summary>
        /// <returns>Lista de carreras con Id y Nombre.</returns>
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

        /// <summary>
        /// Limpia los espacios en blanco para válidar correctamente el nombre duplicado.
        /// </summary>
        /// <returns>Nombre/descripción sin espacios en blanco innecesarios.</returns>
        private string limpiaEspacios(string texto) => Regex.Replace(texto.Trim(), @"\s+", " ");
    }
}
