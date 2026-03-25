using KPIBackend.Application.DTOs.ArchivoPoliticas;
using KPIBackend.Data;
using KPIBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;

namespace KPIBackend.Presentation.Controllers
{
    /// <summary>
    /// Controlador encargado de gestionar el archivo asocaido a una facultad.
    /// </summary>
    /// <remarks>
    /// Permite subir, consultar, descargar y eliminar archivo de políticas
    /// asociado a una facultad específica.
    /// </remarks>
    [Authorize]
    [ApiController]
    [Route("api/facultades/{facultadId}/archivoPoliticas")]
    public class ArchivoPoliticasController: ControllerBase
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Constructor del controlador del archivo de políticas.
        /// </summary>
        /// <param name="context">Configuración para uso de la base de datos.</param>
        public ArchivoPoliticasController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene el archivo de políticas asociada a una facultad.
        /// </summary>
        /// <param name="facultadId">
        /// Identificador de la facultad al que pertenece el archivo de políticas.
        /// </param>
        /// <returns>
        /// Visualización del archivo de políticas asociada a la facultad.
        /// </returns>
        [HttpGet]
        public async Task<ActionResult<List<ArchivoPoliticasDto>>> GetByFacultyId(Guid facultadId)
        {
            var data = await _context.archivoPoliticas
                .Include(d => d.Facultad)
                .Where(e => e.FacultadId == facultadId)
                .Select(d => new ArchivoPoliticasDto
                {
                    Id = d.Id,
                    NombreArchivo = d.NombreArchivo,
                    Tipo = d.Tipo,
                    Contenido = d.Contenido,
                    FacultadId = d.FacultadId,
                    Facultad = d.Facultad.Nombre
                }).ToListAsync();

            if (!await _context.facultades.AnyAsync(i => i.Id == facultadId))
                return BadRequest("El ID de la facultad especificada no existe");

            return Ok(data);
        }

        /// <summary>
        /// Sube un archivo que contiene las políticas de una facultad.
        /// </summary>
        /// <param name="facultadId">
        /// Identificador de la facultad al que se asociará el archivo de políticas.
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
        public async Task<IActionResult> Upload([FromRoute] Guid facultadId, [FromForm] UploadArchivoPoliticasRequest request)
        {
            var file = request?.File;
            // Validaciones explícitas para estados de error HTTP.
            if (file == null || file.Length == 0)
                return BadRequest("Archivo inválido");

            if (!await _context.facultades.AnyAsync(i => i.Id == facultadId))
                return BadRequest("El ID de la facultad especificada no existe");

            // Validación de extensión PDF.
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (string.IsNullOrEmpty(extension) || extension != ".pdf")
                return BadRequest("Solo se permiten archivos con extensión .pdf.");

            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);

            var provider = new FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(file.FileName, out string contentType))
            {
                contentType = "application/octet-stream";
            }

            var archivoPoliticas = new ArchivoPoliticas
            {
                Id = Guid.NewGuid(),
                FacultadId = facultadId,
                NombreArchivo = file.FileName,
                Tipo = contentType,
                Contenido = ms.ToArray()
            };

            _context.archivoPoliticas.Add(archivoPoliticas);
            await _context.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// Elimina el archivo de políticas específico de una facultad.
        /// </summary>
        /// <param name="facultadId">
        /// Identificador de la facultad al que pertenece el archivo de políticas.
        /// </param>
        /// <param name="archivoPoliticasId">
        /// Identificador del archivo de políticas a eliminar.
        /// </param>
        /// <returns>
        /// Resultado de la operación de eliminación.
        /// </returns>
        [HttpDelete("{archivoPoliticasId}")]
        public async Task<IActionResult> Delete(Guid facultadId, Guid archivoPoliticasId)
        {
            var archivoPoliticas = await _context.archivoPoliticas
                .FirstOrDefaultAsync(e => e.Id == archivoPoliticasId && e.FacultadId == facultadId);

            if (archivoPoliticas == null)
                return NotFound("El archivo de las políticas especificado no existe para esta facultad");

            _context.archivoPoliticas.Remove(archivoPoliticas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Descarga el archivo de políticas específico asociado a una facultad.
        /// </summary>
        /// <param name="facultadId">
        /// Identificador de la facultad.
        /// </param>
        /// <param name="archivoPoliticasId">
        /// Identificador del archivo de políticas.
        /// </param>
        /// <returns>
        /// Archivo almacenado.
        /// </returns>
        [HttpGet("{archivoPoliticasId}/download")]
        public async Task<IActionResult> Download(Guid facultadId, Guid archivoPoliticasId)
        {
            var archivoPoliticas = await _context.archivoPoliticas
                .FirstOrDefaultAsync(e =>
                    e.Id == archivoPoliticasId &&
                    e.FacultadId == facultadId);

            if (archivoPoliticas == null)
                return NotFound("El archivo de políticas especificado no existe para esta facultad");

            return File(
                archivoPoliticas.Contenido,
                string.IsNullOrWhiteSpace(archivoPoliticas.Tipo)
                ? "application/octet-stream" : archivoPoliticas.Tipo
            );
        }
    }
}
