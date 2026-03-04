using KPIBackend.Application.DTOs.ListaCombos;
using KPIBackend.Data;
using KPIBackend.Models;
using KPIBackend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KPIBackend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/roles")]
    public class RolesController : BaseController<Rol>
    {
        private readonly AppDbContext _context;

        public RolesController(IBaseRepository<Rol> repository, AppDbContext context) : base(repository) {

            _context = context;

        }
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
