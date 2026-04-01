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
    /// Controlador encargado de gestionar los roles del sistema.
    /// </summary>
    /// <remarks>
    /// Permite realizar operaciones CRUD sobre los roles
    /// y obtener datos simplificados para listas desplegables.
    /// </remarks>
    [Authorize]
    [ApiController]
    [Route("api/roles")]
    public class RolesController : BaseController<Rol>
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// La variable EntityDisplayName de BaseController tomará el nombre colocado para los mensajes.
        /// </summary>
        protected override string EntityDisplayName => "El rol";

        /// <summary>
        /// Constructor del controlador de actividad.
        /// </summary>
        /// <param name="repository">Repositorio del rol.</param>
        /// <param name="context">Configuración para uso de la base de datos.</param>
        public RolesController(IBaseRepository<Rol> repository, AppDbContext context) : base(repository)
        {

            _context = context;

        }

        /// <summary>
        /// Obtiene una lista simplificada de roles.
        /// </summary>
        /// <returns>
        /// Lista de roles con identificador y nombre.
        /// </returns>
        [HttpGet("combo")]
        public async Task<IActionResult> GetCombo()
        {
            var roles = await _context.roles
                .Select(u => new RolComboDTO
                {
                    Id = u.Id,
                    Nombre = u.Nombre
                })
                .ToListAsync();

            return Ok(roles);
        }
    }
}
