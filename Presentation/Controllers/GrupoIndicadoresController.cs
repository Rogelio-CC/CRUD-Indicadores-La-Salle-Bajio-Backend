using KPIBackend.Application.DTOs.ListaCombos;
using KPIBackend.Data;
using KPIBackend.Models;
using KPIBackend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KPIBackend.Controllers
{
    /// <summary>
    /// Controlador encargado de gestionar los grupos de indicadores.
    /// </summary>
    /// <remarks>
    /// Permite realizar operaciones CRUD y obtener listas simplificadas
    /// para componentes de selección en el frontend.
    /// </remarks>
    [Authorize]
    [ApiController]
    [Route("api/grupo-indicadores")]
    public class GrupoIndicadoresController : BaseController<GrupoIndicadores>
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Constructor del controlador de actividad.
        /// </summary>
        /// <param name="repository">Repositorio de la actividad.</param>
        /// <param name="context">Configuración para uso de la base de datos.</param>
        public GrupoIndicadoresController(IBaseRepository<GrupoIndicadores> repository, AppDbContext context) : base(repository)
        {

            _context = context;
        }

        /// <summary>
        /// Obtiene una lista simplificada de grupos de indicadores.
        /// </summary>
        /// <returns>
        /// Lista con identificador y nombre del grupo.
        /// </returns>
        [HttpGet("combo")]
        public async Task<IActionResult> GetCombo()
        {
            var grruposIndicadores = await _context.grupo_indicadores
                .Select(u => new GrupoIndicadoresComboDTO
                {
                    Id = u.Id,
                    Nombre = u.DescripcionGrupo
                })
                .ToListAsync();

            return Ok(grruposIndicadores);
        }
    }
}

