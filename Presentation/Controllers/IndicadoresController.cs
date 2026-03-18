using KPIBackend.Application.DTOs.Directriz;
using KPIBackend.Application.DTOs.Indicador;
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
    [Route("api/indicadores")]
    public class IndicadoresController : BaseController<Indicador>
    {
        private readonly AppDbContext _context;

        public IndicadoresController(IIndicadorRepository repository, AppDbContext context) : base(repository) {

            _context = context;
        }

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
                return NotFound("El indicador no existe");

            return Ok(dto);
        }


        [HttpPost("dto")]
        public async Task<IActionResult> Create(IndicadorCreateUpdateDto dto)
        {
            // Validaciones explícitas (muy importante)
            if (string.IsNullOrWhiteSpace(dto.DescripcionIndicador))
                return BadRequest("La descripción del indicador es obligatoria");

            if(dto.DescripcionIndicador.Length <= 3 || dto.DescripcionIndicador.Length > 50)
                return BadRequest("La descripción del indicador debe tener al menos un carácter y no puede exceder los 50 caracteres");

            if (string.IsNullOrWhiteSpace(dto.FrecuenciaControl))
                return BadRequest("La frecuencia de control del indicador es obligatoria");
            
            if (dto.FrecuenciaControl != "Anual" && dto.FrecuenciaControl != "Mensual" && dto.FrecuenciaControl != "Semanal" && dto.FrecuenciaControl != "Diario")
                return BadRequest("El tipo de frecuencia debe ser 'Anual', 'Mensual', 'Semanal' o 'Diario'");

            if (dto.CantidadEvidencias <= 0)
                return BadRequest("La cantidad de evidencias no puede ser negativa o cero");

            if (dto.FechaCumplimiento < DateTimeOffset.Now.Date)
                return BadRequest("La fecha de cumplimiento no puede ser anterior a la fecha actual");

            if (dto.FechaCumplimiento > DateTimeOffset.Now.Date.AddYears(1))
                return BadRequest("La fecha de cumplimiento no puede ser tan lejana en el futuro");

            if (dto.DirectrizId == Guid.Empty)
                return BadRequest("El ID de la directriz es obligatorio");

            if (dto.GrupoId == Guid.Empty)
                return BadRequest("El ID del grupo de indicadores es obligatorio");

            if (dto.CreadorId == Guid.Empty)
                return BadRequest("El ID del creador es obligatorio");

            if (dto.PeriodoId == Guid.Empty)
                return BadRequest("El ID del periodo es obligatorio");

            if (dto.CarreraId == Guid.Empty)
                return BadRequest("El ID de la carrera es obligatorio");

            if (!await _context.carreras.AnyAsync(f => f.Id == dto.CarreraId))
                return BadRequest("El ID de la carrera especificada no existe");

            if (!await _context.usuarios.AnyAsync(u => u.Id == dto.CreadorId))
                return BadRequest("El ID del creador especificado no existe");

            if (!await _context.periodos_escolares.AnyAsync(p => p.Id == dto.PeriodoId))
                return BadRequest("El ID del periodo especificado no existe");

            if (!await _context.grupo_indicadores.AnyAsync(p => p.Id == dto.GrupoId))
                return BadRequest("El ID del grupo de indicadores especificado no existe");

            if (!await _context.directrices.AnyAsync(p => p.Id == dto.DirectrizId))
                return BadRequest("El ID de la directriz especificada no existe");

            if (await _context.indicadores.AnyAsync(e => e.DescripcionIndicador.ToLower() == dto.DescripcionIndicador.ToLower()))
                return Conflict("Ya existe otro indicador con esa descripción");


            var indicador = new Indicador
            {
                DescripcionIndicador = dto.DescripcionIndicador,
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

        [HttpPut("dto/{id}")]
        public async Task<IActionResult> Update(Guid id, IndicadorCreateUpdateDto dto)
        {
            var indicador = await _context.indicadores.FindAsync(id);

            // Validaciones explícitas (muy importante)
            if (indicador == null)
                return NotFound("El indicador no existe");

            var userIdFromIndicator = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var roleFromIndicator = User.FindFirst(ClaimTypes.Role)?.Value;

            if (roleFromIndicator != "Administrador" && indicador.CreadorId.ToString() != userIdFromIndicator)
                return Forbid();

            if (string.IsNullOrWhiteSpace(dto.DescripcionIndicador))
                return BadRequest("La descripción del indicador es obligatoria");

            if(dto.DescripcionIndicador.Length <= 3 || dto.DescripcionIndicador.Length > 50)
                return BadRequest("La descripción del indicador debe tener al menos un carácter y no puede exceder los 50 caracteres");

            if (string.IsNullOrWhiteSpace(dto.FrecuenciaControl))
                return BadRequest("La frecuencia de control del indicador es obligatoria");
            
            if (dto.FrecuenciaControl != "Anual" && dto.FrecuenciaControl != "Mensual" && dto.FrecuenciaControl != "Semanal" && dto.FrecuenciaControl != "Diario")
                return BadRequest("El tipo de frecuencia debe ser 'Anual', 'Mensual', 'Semanal' o 'Diario'");

            if (dto.CantidadEvidencias <= 0)
                return BadRequest("La cantidad de evidencias no puede ser negativa o cero");

            if (dto.FechaCumplimiento < DateTimeOffset.Now.Date)
                return BadRequest("La fecha de cumplimiento no puede ser anterior a la fecha actual");

            if (dto.FechaCumplimiento > DateTimeOffset.Now.Date.AddYears(1))
                return BadRequest("La fecha de cumplimiento no puede ser tan lejana en el futuro");

            if (dto.DirectrizId == Guid.Empty)
                return BadRequest("El ID de la directriz es obligatorio");

            if (dto.GrupoId == Guid.Empty)
                return BadRequest("El ID del grupo de indicadores es obligatorio");

            if (dto.CreadorId == Guid.Empty)
                return BadRequest("El ID del creador es obligatorio");

            if (dto.PeriodoId == Guid.Empty)
                return BadRequest("El ID del periodo es obligatorio");

            if (dto.CarreraId == Guid.Empty)
                return BadRequest("El ID de la carrera es obligatorio");

            if (!await _context.carreras.AnyAsync(f => f.Id == dto.CarreraId))
                return BadRequest("El ID de la carrera especificada no existe");

            if (!await _context.usuarios.AnyAsync(u => u.Id == dto.CreadorId))
                return BadRequest("El ID del creador especificado no existe");

            if (!await _context.periodos_escolares.AnyAsync(p => p.Id == dto.PeriodoId))
                return BadRequest("El ID del periodo especificado no existe");

            if (!await _context.grupo_indicadores.AnyAsync(p => p.Id == dto.GrupoId))
                return BadRequest("El ID del grupo de indicadores especificado no existe");

            if (!await _context.directrices.AnyAsync(p => p.Id == dto.DirectrizId))
                return BadRequest("El ID de la directriz especificada no existe");

            if (await _context.indicadores.AnyAsync(e => e.DescripcionIndicador.ToLower() == dto.DescripcionIndicador.ToLower() && e.Id != id))
                return Conflict("Ya existe otro indicador con esa descripción");

            // Actualización
            indicador.DescripcionIndicador = dto.DescripcionIndicador;
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

        [HttpGet("combo")]
        public async Task<IActionResult> GetCombo()
        {
            var indicadores = await _context.indicadores
                .Select(u => new IndicadorComboDTO
                {
                    Id = u.Id,
                    Nombre = u.DescripcionIndicador
                })
                .ToListAsync();

            return Ok(indicadores);
        }
    }
}
