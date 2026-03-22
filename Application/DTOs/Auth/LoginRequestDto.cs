namespace KPIBackend.Application.DTOs.Auth
{
     /// <summary>
    /// Solicitud de inicio de sesión.
    /// </summary>
    public class LoginRequestDto
    {
        /// <summary>
        /// Se solicita el inicio de sesión por medio del correo institucional.
        /// </summary>
        public string Email { get; set; } = string.Empty;
    }
}
