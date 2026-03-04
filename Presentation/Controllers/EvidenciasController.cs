using KPIBackend.Application.DTOs.Evidencia;
using KPIBackend.Controllers;
using KPIBackend.Data;
using KPIBackend.Infrastructure.Repositories;
using KPIBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.StaticFiles;

namespace KPIBackend.Presentation.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/indicadores/{indicadorId}/evidencias")]

    public class EvidenciasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EvidenciasController(AppDbContext context)
        {

            _context = context;
        }

        [HttpGet] 
        public async Task<ActionResult<List<EvidenciaDto>>> GetAllDto(Guid indicadorId) 
        { 
            var data = await _context.evidencias
                .Include(d => d.Indicador)
                .Where(e => e.IndicadorId == indicadorId)
                .Select(d => new EvidenciaDto 
                { 
                    Id = d.Id, 
                    NombreArchivo = d.NombreArchivo, 
                    Tipo = d.Tipo, 
                    Contenido = d.Contenido, 
                    IndicadorId = d.IndicadorId, 
                    Indicador = d.Indicador.DescripcionIndicador 
                }).ToListAsync(); 

                if (!await _context.indicadores.AnyAsync(i => i.Id == indicadorId))
                    return BadRequest("El ID del indicador especificado no existe");
            
            return Ok(data); 
        }


        [HttpPost]
        public async Task<IActionResult> Upload(Guid indicadorId, [FromForm] IFormFile file)
        {
            // Validaciones explícitas (muy importante)
            if (file == null || file.Length == 0)
                return BadRequest("Archivo inválido");

/*             if (file.Length > 10 * 1024 * 1024) // 10 MB
                return BadRequest("El archivo excede el tamaño máximo permitido de 10 MB"); */

            if (!await _context.indicadores.AnyAsync(i => i.Id == indicadorId))
                return BadRequest("El ID del indicador especificado no existe");

            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);

            var provider = new FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(file.FileName, out string contentType))
            {
                contentType = "application/octet-stream";
            }

            var evidencia = new Evidencia
            {
                Id = Guid.NewGuid(),
                IndicadorId = indicadorId,
                NombreArchivo = file.FileName,
                Tipo = contentType,
                Contenido = ms.ToArray()
            };

            _context.evidencias.Add(evidencia);
            await _context.SaveChangesAsync();

            // 👇 recalcular otra vez
            await RecalcularIndicador(indicadorId);

            return Ok();
        }

        [HttpDelete("{evidenciaId}")]
        public async Task<IActionResult> Delete(Guid indicadorId, Guid evidenciaId)
        {
            var evidencia = await _context.evidencias
                .FirstOrDefaultAsync(e => e.Id == evidenciaId && e.IndicadorId == indicadorId);

            if (evidencia == null)
                return NotFound("La evidencia especificada no existe para este indicador");

            _context.evidencias.Remove(evidencia);
            await _context.SaveChangesAsync();

            // 👇 recalcular otra vez
            await RecalcularIndicador(indicadorId);

            return NoContent();
        }

        [HttpGet("{evidenciaId}/download")]
        public async Task<IActionResult> Download(Guid indicadorId, Guid evidenciaId)
        {
            var evidencia = await _context.evidencias
                .FirstOrDefaultAsync(e =>
                    e.Id == evidenciaId &&
                    e.IndicadorId == indicadorId);

            if (evidencia == null)
                return NotFound("La evidencia especificada no existe para este indicador");

            return File(
                evidencia.Contenido,
                string.IsNullOrWhiteSpace(evidencia.Tipo)
                ? "application/octet-stream" : evidencia.Tipo
            );
        }

        private async Task RecalcularIndicador(Guid indicadorId)
        {
            var indicador = await _context.indicadores
                .Include(i => i.Evidencias)
                .FirstAsync(i => i.Id == indicadorId);

            var total = indicador.Evidencias.Count;

            indicador.Estandar = indicador.CantidadEvidencias == 0
                ? 0
                : Math.Min(100, (decimal)total / indicador.CantidadEvidencias * 100);

            indicador.IndicadorCompletado = indicador.Estandar >= 100;

            //QUEDA PENDIENTE EL PONER VALOR NULL A LA FECHACUMPLIMIENTO CORRECTAMENTE CUANDO EL ESTANDAR ES MENOR A 100 O INDICADORCOMPLETADO ES IGUAL A FALSE

            indicador.Estandar = Math.Round(indicador.Estandar, 2);

            await _context.SaveChangesAsync();
        }
    }

}
