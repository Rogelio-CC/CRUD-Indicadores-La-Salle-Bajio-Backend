using KPIBackend.Controllers;
using KPIBackend.Models;
using KPIBackend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KPIBackend.Presentation.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/eventosCalendario")]
    public class EventosCalendarioController : BaseController<EventoCalendario>
    {
        public EventosCalendarioController(IBaseRepository<EventoCalendario> repository) : base(repository) { }
    }
}
