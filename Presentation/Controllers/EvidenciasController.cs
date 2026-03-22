using KPIBackend.Application.DTOs.Evidencia;
using KPIBackend.Data;
using KPIBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.StaticFiles;

namespace KPIBackend.Presentation.Controllers
{
    /// <summary>
    /// Controlador encargado de gestionar las evidencias asociadas a un indicador.
    /// </summary>
    /// <remarks>
    /// Permite subir, consultar, descargar y eliminar archivos de evidencia
    /// asociados a indicadores específicos.
    /// </remarks>
    [Authorize]
    [ApiController]
    [Route("api/indicadores/{indicadorId}/evidencias")]

    public class EvidenciasController : ControllerBase
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Constructor del controlador de actividad.
        /// </summary>
        /// <param name="context">Configuración para uso de la base de datos.</param>
        public EvidenciasController(AppDbContext context)
        {

            _context = context;
        }

        /// <summary>
        /// Obtiene todas las evidencias asociadas a un indicador.
        /// </summary>
        /// <param name="indicadorId">
        /// Identificador del indicador al que pertenecen las evidencias.
        /// </param>
        /// <returns>
        /// Lista de evidencias asociadas al indicador.
        /// </returns>
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

        /// <summary>
        /// Sube una nueva evidencia para un indicador.
        /// </summary>
        /// <param name="indicadorId">
        /// Identificador del indicador al que se asociará la evidencia.
        /// </param>
        /// <param name="request">
        /// Archivo enviado desde el cliente.
        /// </param>
        /// <returns>
        /// Resultado de la operación de carga.
        /// </returns>
        /// <remarks>
        /// El archivo se almacena en la base de datos como contenido binario.
        /// </remarks>
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Upload([FromRoute] Guid indicadorId, [FromForm] UploadEvidenciaRequest request)
        {
            var file = request?.File;
            // Validaciones explícitas (muy importante)
            if (file == null || file.Length == 0)
                return BadRequest("Archivo inválido");

            if (file.Length > 10 * 1024 * 1024) // 10 MB
                return BadRequest("El archivo excede el tamaño máximo permitido de 10 MB");

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

        /// <summary>
        /// Elimina una evidencia específica de un indicador.
        /// </summary>
        /// <param name="indicadorId">
        /// Identificador del indicador al que pertenece la evidencia.
        /// </param>
        /// <param name="evidenciaId">
        /// Identificador de la evidencia a eliminar.
        /// </param>
        /// <returns>
        /// Resultado de la operación de eliminación.
        /// </returns>
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

        /// <summary>
        /// Descarga una evidencia específica asociada a un indicador.
        /// </summary>
        /// <param name="indicadorId">
        /// Identificador del indicador.
        /// </param>
        /// <param name="evidenciaId">
        /// Identificador de la evidencia.
        /// </param>
        /// <returns>
        /// Archivo almacenado como evidencia.
        /// </returns>
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

        /// <summary>
        /// Recalcula el estado del indicador después de agregar o eliminar evidencias.
        /// </summary>
        /// <param name="indicadorId">
        /// Identificador del indicador a recalcular.
        /// </param>
        /// <remarks>
        /// Calcula el porcentaje de cumplimiento del indicador en función
        /// del número de evidencias registradas.
        /// </remarks>
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

            indicador.Estandar = Math.Round(indicador.Estandar, 2);

            await _context.SaveChangesAsync();
        }
    }

}
