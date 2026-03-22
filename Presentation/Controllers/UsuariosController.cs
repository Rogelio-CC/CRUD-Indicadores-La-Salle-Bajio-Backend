using KPIBackend.Application.DTOs.Usuarios;
using KPIBackend.Data;
using KPIBackend.Models;
using KPIBackend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KPIBackend.Application.DTOs.ListaCombos;

namespace KPIBackend.Controllers
{
    /// <summary>
    /// Controlador encargado de gestionar los usuarios del sistema.
    /// </summary>
    /// <remarks>
    /// Permite consultar, registrar y actualizar usuarios.
    /// Cada usuario está asociado a un rol, facultad y carrera.
    /// </remarks>
    [Authorize]
    [ApiController]
    [Route("api/usuarios")]
    public class UsuariosController : BaseController<Usuario>
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Constructor del controlador de actividad.
        /// </summary>
        /// <param name="repository">Repositorio del usuario.</param>
        /// <param name="context">Configuración para uso de la base de datos.</param>
        public UsuariosController(IUsuarioRepository repository, AppDbContext context) : base(repository)
        {

            _context = context;
        }

        /// <summary>
        /// Obtiene todos los usuarios registrados en el sistema.
        /// </summary>
        /// <returns>
        /// Lista de usuarios con información de rol, facultad y carrera.
        /// </returns>
        [HttpGet("dto")]
        public async Task<ActionResult<List<UsuarioDto>>> GetAllDto()
        {
            var data = await _context.usuarios
                .Include(d => d.Rol)
                .Include(d => d.Facultad)
                .Include(d => d.Carrera)
                .Select(d => new UsuarioDto
                {
                    Id = d.Id,
                    NombreUsuario = d.NombreUsuario,
                    CorreoInstitucional = d.CorreoInstitucional,
                    TipoUsuario = d.TipoUsuario,
                    RolId = d.RolId,
                    Rol = d.Rol.Nombre,
                    FacultadId = d.FacultadId,
                    Facultad = d.Facultad.Nombre,
                    CarreraId = d.CarreraId,
                    Carrera = d.Carrera.NombreCarrera,
                })
                .ToListAsync();

            return Ok(data);
        }

        /// <summary>
        /// Obtiene un usuario específico por su identificador.
        /// </summary>
        /// <param name="id">
        /// Identificador único del usuario.
        /// </param>
        /// <returns>
        /// Información del usuario.
        /// </returns>
        [HttpGet("dto/{id}")]
        public async Task<IActionResult> GetDtoById(Guid id)
        {
            var dto = await _context.usuarios
            .Include(d => d.Rol)
            .Include(d => d.Facultad)
            .Include(d => d.Carrera)
            .Where(d => d.Id == id)

            .Select(d => new UsuarioDto
            {
                Id = d.Id,
                NombreUsuario = d.NombreUsuario,
                CorreoInstitucional = d.CorreoInstitucional,
                TipoUsuario = d.TipoUsuario,
                RolId = d.RolId,
                Rol = d.Rol.Nombre,
                FacultadId = d.FacultadId,
                Facultad = d.Facultad.Nombre,
                CarreraId = d.CarreraId,
                Carrera = d.Carrera.NombreCarrera,
            })
            .FirstOrDefaultAsync();

            if (dto == null)
                return NotFound("El usuario no existe");

            return Ok(dto);
        }

        /// <summary>
        /// Registra un nuevo usuario en el sistema.
        /// </summary>
        /// <param name="dto">
        /// Datos necesarios para crear el usuario.
        /// </param>
        /// <returns>
        /// Resultado de la operación.
        /// </returns>
        /// <remarks>
        /// Se validan campos como:
        /// - nombre de usuario
        /// - correo institucional
        /// - tipo de usuario
        /// - relaciones con rol, facultad y carrera
        /// </remarks>
        [HttpPost("dto")]
        public async Task<IActionResult> Create(UsuarioCreateUpdateDto dto)
        {
            // Validaciones explícitas para errores de estado HTTP.
            if (dto == null)
                return BadRequest("Los datos del usuario no pueden estar vacíos");

            if (dto.NombreUsuario.Length <= 3 || dto.NombreUsuario.Length > 100)
                return BadRequest("El nombre de usuario debe tener al menos un carácter y no puede exceder los 100 caracteres");

            if (dto.CorreoInstitucional.Length <= 3 || dto.CorreoInstitucional.Length > 100)
                return BadRequest("El correo institucional debe tener al menos un carácter y no puede exceder los 100 caracteres");

            if (!dto.CorreoInstitucional.Contains("@"))
                return BadRequest("El correo institucional debe ser una dirección de correo electrónico válida");

            if (dto.TipoUsuario != "Administrador" && dto.TipoUsuario != "Jefe_de_Pregrado" && dto.TipoUsuario != "Director" && dto.TipoUsuario != "Investigador" && dto.TipoUsuario != "Maestro")
                return BadRequest("El tipo de usuario debe ser 'Administrador', 'Jefe_de_Pregrado', 'Director', 'Investigador' o 'Maestro'");

            if (!await _context.roles.AnyAsync(f => f.Id == dto.RolId))
                return BadRequest("El ID del rol especificado no existe");

            if (!await _context.facultades.AnyAsync(u => u.Id == dto.FacultadId))
                return BadRequest("El ID de la facultad especificada no existe");

            if (!await _context.carreras.AnyAsync(p => p.Id == dto.CarreraId))
                return BadRequest("El ID de la carrera especificada no existe");

            if (await _context.usuarios.AnyAsync(e => e.NombreUsuario.ToLower() == dto.NombreUsuario.ToLower()))
                return Conflict("Ya existe otro usuario con ese nombre de usuario");

            if (await _context.usuarios.AnyAsync(e => e.CorreoInstitucional.ToLower() == dto.CorreoInstitucional.ToLower()))
                return Conflict("Ya existe otro usuario con ese correo institucional");

            var usuario = new Usuario
            {
                NombreUsuario = dto.NombreUsuario,
                CorreoInstitucional = dto.CorreoInstitucional,
                TipoUsuario = dto.TipoUsuario,
                RolId = dto.RolId,
                FacultadId = dto.FacultadId,
                CarreraId = dto.CarreraId,
            };

            _context.usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// Actualiza la información de un usuario existente.
        /// </summary>
        /// <param name="id">
        /// Identificador del usuario.
        /// </param>
        /// <param name="dto">
        /// Datos actualizados del usuario.
        /// </param>
        /// <returns>
        /// Resultado de la operación de actualización.
        /// </returns>
        [HttpPut("dto/{id}")]
        public async Task<IActionResult> Update(Guid id, UsuarioCreateUpdateDto dto)
        {
            var usuario = await _context.usuarios.FindAsync(id);

            // Validaciones explícitas para errores de estado HTTP.
            if (usuario == null)
                return NotFound("El usuario no existe");

            if (dto == null)
                return BadRequest("Los datos del usuario no pueden estar vacíos");

            if (dto.NombreUsuario.Length <= 3 || dto.NombreUsuario.Length > 100)
                return BadRequest("El nombre de usuario debe tener al menos un carácter y no puede exceder los 100 caracteres");

            if (dto.CorreoInstitucional.Length <= 3 || dto.CorreoInstitucional.Length > 100)
                return BadRequest("El correo institucional debe tener al menos un carácter y no puede exceder los 100 caracteres");

            if (!dto.CorreoInstitucional.Contains("@"))
                return BadRequest("El correo institucional debe ser una dirección de correo electrónico válida");

            if (dto.TipoUsuario != "Administrador" && dto.TipoUsuario != "Jefe_de_Pregrado" && dto.TipoUsuario != "Director" && dto.TipoUsuario != "Investigador" && dto.TipoUsuario != "Maestro")
                return BadRequest("El tipo de usuario debe ser 'Administrador', 'Jefe_de_Pregrado', 'Director', 'Investigador' o 'Maestro'");

            if (!await _context.roles.AnyAsync(f => f.Id == dto.RolId))
                return BadRequest("El ID del rol especificado no existe");

            if (!await _context.facultades.AnyAsync(u => u.Id == dto.FacultadId))
                return BadRequest("El ID de la facultad especificada no existe");

            if (!await _context.carreras.AnyAsync(p => p.Id == dto.CarreraId))
                return BadRequest("El ID de la carrera especificada no existe");

            if (await _context.usuarios.AnyAsync(e => e.CorreoInstitucional.ToLower() == dto.CorreoInstitucional.ToLower() && e.Id != id))
                return Conflict("Ya existe otro usuario con ese correo institucional");

            if (await _context.usuarios.AnyAsync(e => e.NombreUsuario.ToLower() == dto.NombreUsuario.ToLower() && e.Id != id))
                return Conflict("Ya existe otro usuario con ese nombre de usuario");

            usuario.NombreUsuario = dto.NombreUsuario;
            usuario.CorreoInstitucional = dto.CorreoInstitucional;
            usuario.TipoUsuario = dto.TipoUsuario;
            usuario.RolId = dto.RolId;
            usuario.FacultadId = dto.FacultadId;
            usuario.CarreraId = dto.CarreraId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Obtiene una lista simplificada de usuarios.
        /// </summary>
        /// <returns>
        /// Lista con identificador y nombre del usuario.
        /// </returns>
        [HttpGet("combo")]
        public async Task<IActionResult> GetCombo()
        {
            var usuarios = await _context.usuarios
                .Select(u => new UsuarioComboDTO
                {
                    Id = u.Id,
                    Nombre = u.NombreUsuario
                })
                .ToListAsync();

            return Ok(usuarios);
        }

        /// <summary>
        /// Obtiene un usuario mediante su correo institucional.
        /// </summary>
        /// <param name="email">
        /// Correo institucional del usuario.
        /// </param>
        /// <returns>
        /// Información del usuario encontrado.
        /// </returns>
        [HttpGet("dto/email/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var user = await _context.usuarios
                .Include(u => u.Rol)
                .Include(u => u.Facultad)
                .Include(u => u.Carrera)
                .FirstOrDefaultAsync(u => u.CorreoInstitucional == email);

            if (user == null)
                return NotFound("El usuario no existe");

            return Ok(new UsuarioDto
            {
                Id = user.Id,
                NombreUsuario = user.NombreUsuario,
                CorreoInstitucional = user.CorreoInstitucional,
                TipoUsuario = user.TipoUsuario,
                Rol = user.Rol.Nombre,
                Facultad = user.Facultad.Nombre,
                Carrera = user.Carrera.NombreCarrera
            });
        }

    }

}
