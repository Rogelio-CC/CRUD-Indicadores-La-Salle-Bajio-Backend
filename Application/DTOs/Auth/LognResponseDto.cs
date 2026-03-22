namespace KPIBackend.Application.DTOs.Auth
{
    /// <summary>
    /// Respuesta generada después de una autenticación exitosa.
    /// </summary>
    public class LoginResponseDto
    {
        /// <summary>
        /// Token JWT para autenticar  solicitudes y autorizar al acceso a la aplicación.
        /// </summary>
        public string Token { get; set; } = string.Empty;
    }
}
