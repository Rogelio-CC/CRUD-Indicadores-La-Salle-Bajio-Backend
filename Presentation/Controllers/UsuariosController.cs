using KPIBackend.Application.DTOs.Directriz;
using KPIBackend.Application.DTOs.Usuarios;
using KPIBackend.Data;
using KPIBackend.Models;
using KPIBackend.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaAutenticador2.Shared.DTOS.ListaCombos;

namespace KPIBackend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/usuarios")]
    public class UsuariosController : BaseController<Usuario>
    {
        private readonly AppDbContext _context;

        public UsuariosController(IUsuarioRepository repository, AppDbContext context) : base(repository) {

            _context = context;
        }

        
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


        [HttpPost("dto")]
        public async Task<IActionResult> Create(UsuarioCreateUpdateDto dto)
        {
            // Validaciones explícitas (muy importante)
            if (dto == null)
                return BadRequest("Los datos del usuario no pueden estar vacíos");

            if(dto.NombreUsuario.Length <= 3 || dto.NombreUsuario.Length > 100)
                return BadRequest("El nombre de usuario debe tener al menos un carácter y no puede exceder los 100 caracteres");

            if(dto.CorreoInstitucional.Length <= 3 || dto.CorreoInstitucional.Length > 100)
                return BadRequest("El correo institucional debe tener al menos un carácter y no puede exceder los 100 caracteres");

            if(!dto.CorreoInstitucional.Contains("@"))
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

        [HttpPut("dto/{id}")]
        public async Task<IActionResult> Update(Guid id, UsuarioCreateUpdateDto dto)
        {
            var usuario = await _context.usuarios.FindAsync(id);

             // Validaciones explícitas (muy importante)
            if (usuario == null)
                return NotFound("El usuario no existe");

            if (dto == null)
                return BadRequest("Los datos del usuario no pueden estar vacíos");

            if(dto.NombreUsuario.Length <= 3 || dto.NombreUsuario.Length > 100)
                return BadRequest("El nombre de usuario debe tener al menos un carácter y no puede exceder los 100 caracteres");

            if(dto.CorreoInstitucional.Length <= 3 || dto.CorreoInstitucional.Length > 100)
                return BadRequest("El correo institucional debe tener al menos un carácter y no puede exceder los 100 caracteres");

            if(!dto.CorreoInstitucional.Contains("@"))
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

            // Actualización
            usuario.NombreUsuario = dto.NombreUsuario;
            usuario.CorreoInstitucional = dto.CorreoInstitucional;
            usuario.TipoUsuario = dto.TipoUsuario;
            usuario.RolId = dto.RolId;
            usuario.FacultadId = dto.FacultadId;
            usuario.CarreraId = dto.CarreraId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

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
