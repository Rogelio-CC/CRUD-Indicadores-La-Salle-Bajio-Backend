using KPIBackend.Application.DTOs.Indicador;
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
    /// Controlador encargado de gestionar los indicadores del sistema.
    /// </summary>
    /// <remarks>
    /// Los indicadores representan métricas de seguimiento asociadas
    /// a directrices, grupos de indicadores, periodos escolares y carreras.
    /// </remarks>
    [Authorize]
    [ApiController]
    [Route("api/indicadores")]
    public class IndicadoresController : BaseController<Indicador>
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Constructor del controlador de actividad.
        /// </summary>
        /// <param name="repository">Repositorio del indicador.</param>
        /// <param name="context">Configuración para uso de la base de datos.</param>
        public IndicadoresController(IIndicadorRepository repository, AppDbContext context) : base(repository)
        {

            _context = context;
        }

        /// <summary>
        /// Obtiene todos los indicadores del sistema en formato DTO.
        /// </summary>
        /// <returns>
        /// Lista de indicadores con información relacionada
        /// como directriz, grupo, creador, periodo y carrera.
        /// </returns>
        [HttpGet("dto")]
        public async Task<ActionResult<List<IndicadorDto>>> GetAllDto()
        {
            var data = await _context.indicadores
                .Include(d => d.Directriz)
                .Include(d => d.Grupo)
                .Include(d => d.Creador)
                .Include(d => d.Periodo)
                .Include(d => d.Carrera)
                .Select(d => new IndicadorDto
                {
                    Id = d.Id,
                    DescripcionIndicador = d.DescripcionIndicador,
                    Estandar = d.Estandar,
                    FrecuenciaControl = d.FrecuenciaControl,
                    CantidadEvidencias = d.CantidadEvidencias,
                    IndicadorCompletado = d.IndicadorCompletado,
                    AccionCorrectiva = d.AccionCorrectiva,
                    FechaEmision = d.FechaEmision,
                    FechaCumplimiento = d.FechaCumplimiento,
                    DirectrizId = d.DirectrizId,
                    Directriz = d.Directriz.Descripcion,
                    GrupoId = d.GrupoId,
                    Grupo = d.Grupo.DescripcionGrupo,
                    CreadorId = d.CreadorId,
                    Creador = d.Creador.NombreUsuario,
                    ResponsableAccionCorrectivaId = d.ResponsableAccionCorrectivaId,
                    ResponsableAccionCorrectiva = d.ResponsableAccionCorrectiva!.NombreUsuario,
                    PeriodoId = d.PeriodoId,
                    Periodo = d.Periodo.Nombre,
                    CarreraId = d.CarreraId,
                    Carrera = d.Carrera.NombreCarrera


                })
                .ToListAsync();

            return Ok(data);
        }

        /// <summary>
        /// Obtiene un indicador específico por su identificador.
        /// </summary>
        /// <param name="id">
        /// Identificador único del indicador.
        /// </param>
        /// <returns>
        /// Indicador encontrado con información relacionada.
        /// </returns>
        [HttpGet("dto/{id}")]
        public async Task<IActionResult> GetDtoById(Guid id)
        {
            var dto = await _context.indicadores
            .Include(d => d.Directriz)
                .Include(d => d.Grupo)
                .Include(d => d.Creador)
                .Include(d => d.Periodo)
                .Include(d => d.Carrera)
            .Where(d => d.Id == id)

            .Select(d => new IndicadorDto
            {
                Id = d.Id,
                DescripcionIndicador = d.DescripcionIndicador,
                Estandar = d.Estandar,
                FrecuenciaControl = d.FrecuenciaControl,
                CantidadEvidencias = d.CantidadEvidencias,
                IndicadorCompletado = d.IndicadorCompletado,
                AccionCorrectiva = d.AccionCorrectiva,
                FechaEmision = d.FechaEmision,
                FechaCumplimiento = d.FechaCumplimiento,
                DirectrizId = d.DirectrizId,
                Directriz = d.Directriz.Descripcion,
                GrupoId = d.GrupoId,
                Grupo = d.Grupo.DescripcionGrupo,
                CreadorId = d.CreadorId,
                Creador = d.Creador.NombreUsuario,
                ResponsableAccionCorrectivaId = d.ResponsableAccionCorrectivaId,
                ResponsableAccionCorrectiva = d.ResponsableAccionCorrectiva!.NombreUsuario,
                PeriodoId = d.PeriodoId,
                Periodo = d.Periodo.Nombre,
                CarreraId = d.CarreraId,
                Carrera = d.Carrera.NombreCarrera
            })
            .FirstOrDefaultAsync();

            if (dto == null)
                return NotFound("El indicador no existe.");

            return Ok(dto);
        }

        /// <summary>
        /// Crea un nuevo indicador en el sistema.
        /// </summary>
        /// <param name="dto">
        /// Datos necesarios para registrar el indicador.
        /// </param>
        /// <returns>
        /// Indicador creado.
        /// </returns>
        /// <remarks>
        /// Se realizan validaciones sobre la descripción,
        /// frecuencia de control, fechas y relaciones con otras entidades.
        /// </remarks>
        [HttpPost("dto")]
        public async Task<IActionResult> Create(IndicadorCreateUpdateDto dto)
        {
            // Validaciones explícitas para errores de estado HTTP.
            if (string.IsNullOrWhiteSpace(dto.DescripcionIndicador))
                return BadRequest("La descripción del indicador es obligatoria.");

            if (dto.DescripcionIndicador.Length <= 3 || dto.DescripcionIndicador.Length > 500)
                return BadRequest("La descripción del indicador debe tener al menos 3 caracteres y no puede exceder los 500 caracteres.");

            if (string.IsNullOrWhiteSpace(dto.FrecuenciaControl))
                return BadRequest("La frecuencia de control del indicador es obligatoria.");

            if (dto.FrecuenciaControl != "Anual" && dto.FrecuenciaControl != "Mensual" && dto.FrecuenciaControl != "Semanal" && dto.FrecuenciaControl != "Diario")
                return BadRequest("El tipo de frecuencia debe ser 'Anual', 'Mensual', 'Semanal' o 'Diario'.");

            if (dto.CantidadEvidencias <= 0)
                return BadRequest("La cantidad de evidencias no puede ser negativa o cero.");

            if (dto.FechaCumplimiento < DateTimeOffset.Now.Date)
                return BadRequest("La fecha de cumplimiento no puede ser anterior a la fecha actual.");

            if (dto.FechaCumplimiento >= DateTimeOffset.Now.Date.AddYears(1))
                return BadRequest("La fecha de cumplimiento no puede ser tan lejana a 1 año o más en el futuro.");

            if (dto.DirectrizId == Guid.Empty)
                return BadRequest("El ID de la directriz es obligatorio.");

            if (dto.GrupoId == Guid.Empty)
                return BadRequest("El ID del grupo de indicadores es obligatorio.");

            if (dto.CreadorId == Guid.Empty)
                return BadRequest("El ID del creador es obligatorio.");

            if (dto.PeriodoId == Guid.Empty)
                return BadRequest("El ID del periodo es obligatorio.");

            if (dto.CarreraId == Guid.Empty)
                return BadRequest("El ID de la carrera es obligatorio.");

            if (!await _context.carreras.AnyAsync(f => f.Id == dto.CarreraId))
                return BadRequest("El ID de la carrera especificada no existe.");

            if (!await _context.usuarios.AnyAsync(u => u.Id == dto.CreadorId))
                return BadRequest("El ID del creador especificado no existe.");

            if (!await _context.periodos_escolares.AnyAsync(p => p.Id == dto.PeriodoId))
                return BadRequest("El ID del periodo especificado no existe.");

            if (!await _context.grupo_indicadores.AnyAsync(p => p.Id == dto.GrupoId))
                return BadRequest("El ID del grupo de indicadores especificado no existe.");

            if (!await _context.directrices.AnyAsync(p => p.Id == dto.DirectrizId))
                return BadRequest("El ID de la directriz especificada no existe.");

            // Limpieza de espacios en blanco para validar correctamente la descripción duplicada.
            var creacionDescripcionIndicador = limpiaEspacios(dto.DescripcionIndicador);

            // Validación de la descripción del indicador duplicado sin importar mayúsculas, minúsculas o espacios adicionales.
            var creacionDescripcionIndicadorParaValidar = creacionDescripcionIndicador.ToLower();

            if (await _context.indicadores.AnyAsync(e => e.CarreraId == dto.CarreraId && e.DescripcionIndicador.ToLower() == creacionDescripcionIndicadorParaValidar))
                return Conflict("Esta carrera ya cuenta con este indicador.");


            var indicador = new Indicador
            {
                DescripcionIndicador = creacionDescripcionIndicador, // Se asigna la descripción limpia sin espacios adicionales.
                Estandar = dto.Estandar,
                FrecuenciaControl = dto.FrecuenciaControl,
                CantidadEvidencias = dto.CantidadEvidencias,
                IndicadorCompletado = dto.IndicadorCompletado,
                AccionCorrectiva = dto.AccionCorrectiva,
                FechaCumplimiento = dto.FechaCumplimiento?.ToUniversalTime(),
                DirectrizId = dto.DirectrizId,
                GrupoId = dto.GrupoId,
                CreadorId = dto.CreadorId,
                ResponsableAccionCorrectivaId = dto.ResponsableAccionCorrectivaId,
                PeriodoId = dto.PeriodoId,
                CarreraId = dto.CarreraId,
            };

            _context.indicadores.Add(indicador);
            await _context.SaveChangesAsync();


            return Ok(new IndicadorDto
            {
                Id = indicador.Id,
                DescripcionIndicador = indicador.DescripcionIndicador,
                Estandar = indicador.Estandar,
                CantidadEvidencias = indicador.CantidadEvidencias,
                IndicadorCompletado = indicador.IndicadorCompletado,
                AccionCorrectiva = indicador.AccionCorrectiva,
                FechaCumplimiento = indicador.FechaCumplimiento,
                DirectrizId = indicador.DirectrizId,
                GrupoId = indicador.GrupoId,
                CreadorId = indicador.CreadorId,
                ResponsableAccionCorrectivaId = dto.ResponsableAccionCorrectivaId,
                PeriodoId = indicador.PeriodoId,
                CarreraId = indicador.CarreraId,

            });
        }

        /// <summary>
        /// Actualiza un indicador existente.
        /// </summary>
        /// <param name="id">
        /// Identificador del indicador.
        /// </param>
        /// <param name="dto">
        /// Datos actualizados del indicador.
        /// </param>
        /// <returns>
        /// Resultado de la operación de actualización.
        /// </returns>
        /// <remarks>
        /// Solo el creador del indicador o un administrador puede modificarlo.
        /// </remarks>
        [HttpPut("dto/{id}")]
        public async Task<IActionResult> Update(Guid id, IndicadorCreateUpdateDto dto)
        {
            var indicador = await _context.indicadores.FindAsync(id);

            // Validaciones explícitas para errores de estado HTTP.
            if (indicador == null)
                return NotFound("El indicador no existe.");

            var userIdFromIndicator = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var roleFromIndicator = User.FindFirst(ClaimTypes.Role)?.Value;

            if (roleFromIndicator != "Administrador" && indicador.CreadorId.ToString() != userIdFromIndicator)
                return Forbid();

            if (string.IsNullOrWhiteSpace(dto.DescripcionIndicador))
                return BadRequest("La descripción del indicador es obligatoria.");

            if (dto.DescripcionIndicador.Length <= 3 || dto.DescripcionIndicador.Length > 500)
                return BadRequest("La descripción del indicador debe tener al menos 3 caracteres y no puede exceder los 500 caracteres.");

            if (string.IsNullOrWhiteSpace(dto.FrecuenciaControl))
                return BadRequest("La frecuencia de control del indicador es obligatoria.");

            if (dto.FrecuenciaControl != "Anual" && dto.FrecuenciaControl != "Mensual" && dto.FrecuenciaControl != "Semanal" && dto.FrecuenciaControl != "Diario")
                return BadRequest("El tipo de frecuencia debe ser 'Anual', 'Mensual', 'Semanal' o 'Diario'.");

            if (dto.CantidadEvidencias <= 0)
                return BadRequest("La cantidad de evidencias no puede ser negativa o cero.");

            if (dto.FechaCumplimiento < DateTimeOffset.Now.Date)
                return BadRequest("La fecha de cumplimiento no puede ser anterior a la fecha actual.");

            if (dto.FechaCumplimiento >= DateTimeOffset.Now.Date.AddYears(1))
                return BadRequest("La fecha de cumplimiento no puede ser tan lejana a 1 año o más en el futuro.");

            if (dto.DirectrizId == Guid.Empty)
                return BadRequest("El ID de la directriz es obligatorio.");

            if (dto.GrupoId == Guid.Empty)
                return BadRequest("El ID del grupo de indicadores es obligatorio.");

            if (dto.CreadorId == Guid.Empty)
                return BadRequest("El ID del creador es obligatorio.");

            if (dto.PeriodoId == Guid.Empty)
                return BadRequest("El ID del periodo es obligatorio.");

            if (dto.CarreraId == Guid.Empty)
                return BadRequest("El ID de la carrera es obligatorio.");

            if (!await _context.carreras.AnyAsync(f => f.Id == dto.CarreraId))
                return BadRequest("El ID de la carrera especificada no existe.");

            if (!await _context.usuarios.AnyAsync(u => u.Id == dto.CreadorId))
                return BadRequest("El ID del creador especificado no existe.");

            if (!await _context.periodos_escolares.AnyAsync(p => p.Id == dto.PeriodoId))
                return BadRequest("El ID del periodo especificado no existe.");

            if (!await _context.grupo_indicadores.AnyAsync(p => p.Id == dto.GrupoId))
                return BadRequest("El ID del grupo de indicadores especificado no existe.");

            if (!await _context.directrices.AnyAsync(p => p.Id == dto.DirectrizId))
                return BadRequest("El ID de la directriz especificada no existe.");

            var actualizacionDescripcionIndicador = limpiaEspacios(dto.DescripcionIndicador);

            var actualizacionDescripcionIndicadorParaValidar = actualizacionDescripcionIndicador.ToLower();

            if (await _context.indicadores.AnyAsync(e => e.CarreraId == dto.CarreraId && e.DescripcionIndicador.ToLower() == actualizacionDescripcionIndicadorParaValidar && e.Id != id))
                return Conflict("Esta carrera ya cuenta con este indicador.");

            indicador.DescripcionIndicador = actualizacionDescripcionIndicador;
            indicador.Estandar = dto.Estandar;
            indicador.FrecuenciaControl = dto.FrecuenciaControl;
            indicador.CantidadEvidencias = dto.CantidadEvidencias;
            indicador.IndicadorCompletado = dto.IndicadorCompletado;
            indicador.AccionCorrectiva = dto.AccionCorrectiva;
            indicador.FechaCumplimiento = dto.FechaCumplimiento?.ToUniversalTime();
            indicador.DirectrizId = dto.DirectrizId;
            indicador.GrupoId = dto.GrupoId;
            indicador.CreadorId = dto.CreadorId;
            indicador.ResponsableAccionCorrectivaId = dto.ResponsableAccionCorrectivaId;
            indicador.PeriodoId = dto.PeriodoId;
            indicador.CarreraId = dto.CarreraId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Obtiene una lista simplificada de indicadores.
        /// </summary>
        /// <returns>
        /// Lista de indicadores con identificador y descripción.
        /// </returns>
        [HttpGet("combo")]
        public async Task<IActionResult> GetCombo()
        {
            var indicadores = await _context.indicadores
                .Select(u => new IndicadorComboDTO
                {
                    Id = u.Id,
                    Nombre = u.DescripcionIndicador,
                    NombreCarrera = u.Carrera.NombreCarrera
                })
                .ToListAsync();

            return Ok(indicadores);
        }

        /// <summary>
        /// Limpia los espacios en blanco para válidar correctamente el nombre duplicado.
        /// </summary>
        /// <returns>Nombre/descripción sin espacios en blanco innecesarios.</returns>
        private string limpiaEspacios(string texto) => Regex.Replace(texto.Trim(), @"\s+", " ");
    }
}
