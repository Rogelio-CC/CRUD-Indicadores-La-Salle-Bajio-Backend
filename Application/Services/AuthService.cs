using KPIBackend.Application.DTOs.Auth;
using KPIBackend.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KPIBackend.Application.Services
{
    /// <summary>
    /// Servicio para autenticación de usuarios.
    /// </summary>
    public class AuthService
    {
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly IConfiguration _config;

        /// <summary>
        /// Inicializa una nueva instancia del servicio de autenticación.
        /// </summary>
        /// <param name="usuarioRepo">Repositorio de usuarios.</param>
        /// <param name="config">Configuración de la aplicación.</param>
        public AuthService(IUsuarioRepository usuarioRepo, IConfiguration config)
        {
            _usuarioRepo = usuarioRepo;
            _config = config;
        }

        /// <summary>
        /// Realiza el login del usuario y genera un token JWT.
        /// </summary>
        /// <param name="request">Datos de la solicitud de login.</param>
        /// <returns>Respuesta con el token de autenticación.</returns>
        public LoginResponseDto Login(LoginRequestDto request)
        {
            var usuario = _usuarioRepo.GetByCorreoAsync(request.Email).Result;

            if (usuario == null)
                throw new UnauthorizedAccessException("Usuario no encontrado");

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Email, usuario.CorreoInstitucional),
                new Claim(ClaimTypes.Role, usuario.TipoUsuario)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]!)
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(4),
                signingCredentials: creds
            );

            return new LoginResponseDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }
    }
}
