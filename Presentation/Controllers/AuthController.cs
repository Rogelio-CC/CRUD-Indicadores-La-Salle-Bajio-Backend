using Microsoft.AspNetCore.Mvc;
using KPIBackend.Application.Services;
using KPIBackend.Application.DTOs.Auth;


namespace KPIBackend.Controllers
{
    /// <summary>
    /// Controlador encargado de gestionar la autenticación de usuarios.
    /// </summary>
    /// <remarks>
    /// Permite iniciar sesión y generar tokens de autenticación.
    /// </remarks>
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        /// <summary>
        /// Constructor del controlador base.
        /// </summary>
        /// <param name="authService">Servicio de autenticación y autorización del usuario.</param>
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Autentica a un usuario utilizando sus credenciales.
        /// </summary>
        /// <param name="request">Datos de inicio de sesión.</param>
        /// <returns>Token de autenticación si las credenciales son válidas.</returns>
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto request)
        {
            try
            {
                var response = _authService.Login(request);
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
