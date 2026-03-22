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
    /// Controlador encargado de gestionar las facultades del sistema.
    /// </summary>
    /// <remarks>
    /// Permite realizar operaciones CRUD sobre las facultades
    /// y obtener datos simplificados para listas desplegables.
    /// </remarks>
    [Authorize]
    [ApiController]
    [Route("api/facultades")]
    public class FacultadesController : BaseController<Facultad>
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Constructor del controlador de actividad.
        /// </summary>
        /// <param name="repository">Repositorio de la actividad.</param>
        /// <param name="context">Configuración para uso de la base de datos.</param>
        public FacultadesController(IFacultadRepository repository, AppDbContext context) : base(repository)
        {

            _context = context;
        }

        /// <summary>
        /// Obtiene una lista simplificada de facultades para utilizar en combos del frontend.
        /// </summary>
        /// <returns>
        /// Lista de facultades con identificador y nombre.
        /// </returns>
        [HttpGet("combo")]
        public async Task<IActionResult> GetCombo()
        {
            var facultades = await _context.facultades
                .Select(u => new FacultadComboDTO
                {
                    Id = u.Id,
                    Nombre = u.Nombre
                })
                .ToListAsync();

            return Ok(facultades);
        }
    }
}
