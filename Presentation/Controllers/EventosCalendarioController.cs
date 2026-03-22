using KPIBackend.Controllers;
using KPIBackend.Models;
using KPIBackend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KPIBackend.Presentation.Controllers
{
    /// <summary>
    /// Controlador encargado de gestionar los eventos del calendario del sistema.
    /// </summary>
    /// <remarks>
    /// Permite realizar operaciones CRUD sobre los eventos del calendario.
    /// Hereda las operaciones básicas desde BaseController.
    /// </remarks>
    [Authorize]
    [ApiController]
    [Route("api/eventosCalendario")]
    public class EventosCalendarioController : BaseController<EventoCalendario>
    {
        /// <summary>
        /// Constructor del controlador de eventos del calendario.
        /// </summary>
        /// <param name="repository">
        /// Repositorio genérico encargado de manejar las operaciones de persistencia de los eventos.
        /// </param>
        public EventosCalendarioController(IBaseRepository<EventoCalendario> repository) : base(repository) { }
    }
}
