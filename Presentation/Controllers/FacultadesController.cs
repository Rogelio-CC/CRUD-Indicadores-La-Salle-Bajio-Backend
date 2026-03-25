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

        /// <summary>
        /// Crea una nueva facultad en el sistema.
        /// </summary>
        /// <param name="facultad">
        /// Datos necesarios para registrar la facultad.
        /// </param>
        /// <returns>
        /// Facultad creada.
        /// </returns>
        [HttpPost("crear")]
        public new async Task<IActionResult> Create(Facultad facultad)
        {
            var facultadcreado = new Facultad
            {
                Nombre = facultad.Nombre,
                Mision = facultad.Mision,
                Vision = facultad.Vision,
                Slogan = facultad.Slogan,
            };

            _context.facultades.Add(facultadcreado);
            await _context.SaveChangesAsync();


            return Ok(new Facultad
            {
                Id = facultadcreado.Id,
                Nombre = facultadcreado.Nombre,
                Mision = facultadcreado.Mision,
                Vision = facultadcreado.Vision,
                Slogan = facultadcreado.Slogan,
            });
        }
    }
}
