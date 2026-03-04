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
    [Route("api/periodos-escolares")]
    public class PeriodosEscolaresController : BaseController<PeriodoEscolar>
    {
        private readonly AppDbContext _context;
        public PeriodosEscolaresController(IPeriodoEscolarRepository repository, AppDbContext context) : base(repository) {

            _context = context;
        }

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
