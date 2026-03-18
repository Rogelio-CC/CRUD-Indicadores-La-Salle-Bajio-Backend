using KPIBackend.Data;
using KPIBackend.Models;
using KPIBackend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaAutenticador2.Shared.DTOS.ListaCombos;

namespace KPIBackend.Controllers
{
    [Authorize]
    [ApiController] 
    [Route("api/facultades")]
    public class FacultadesController : BaseController<Facultad>
    {
        private readonly AppDbContext _context;

        public FacultadesController(IFacultadRepository repository, AppDbContext context) : base(repository) {

            _context = context;
        }

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
