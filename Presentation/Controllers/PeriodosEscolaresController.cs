using KPIBackend.Data;
using KPIBackend.Models;
using KPIBackend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KPIBackend.Application.DTOs.ListaCombos;

namespace KPIBackend.Controllers
{
    /// <summary>
    /// Controlador encargado de gestionar los periodos escolares del sistema.
    /// </summary>
    /// <remarks>
    /// Permite realizar operaciones CRUD sobre periodos escolares
    /// y obtener listas simplificadas para componentes de selección.
    /// </remarks>
    [Authorize]
    [ApiController]
    [Route("api/periodos-escolares")]
    public class PeriodosEscolaresController : BaseController<PeriodoEscolar>
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Constructor del controlador de actividad.
        /// </summary>
        /// <param name="repository">Repositorio del período escolar.</param>
        /// <param name="context">Configuración para uso de la base de datos.</param>
        public PeriodosEscolaresController(IPeriodoEscolarRepository repository, AppDbContext context) : base(repository)
        {

            _context = context;
        }
        /// <summary>
        /// Obtiene una lista simplificada de periodos escolares.
        /// </summary>
        /// <returns>
        /// Lista de periodos con identificador y nombre.
        /// </returns>
        [HttpGet("combo")]
        public async Task<IActionResult> GetCombo()
        {
            var periodosEscolares = await _context.periodos_escolares
                .Select(u => new PeriodoEscolarComboDTO
                {
                    Id = u.Id,
                    Nombre = u.Nombre
                })
                .ToListAsync();

            return Ok(periodosEscolares);
        }
    }
}
