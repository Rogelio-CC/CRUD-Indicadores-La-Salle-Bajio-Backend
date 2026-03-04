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
    [Route("api/grupo-indicadores")]
    public class GrupoIndicadoresController : BaseController<GrupoIndicadores>
    {
        private readonly AppDbContext _context;
        public GrupoIndicadoresController(IBaseRepository<GrupoIndicadores> repository, AppDbContext context) : base(repository) { 
        
            _context = context;
        }

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

