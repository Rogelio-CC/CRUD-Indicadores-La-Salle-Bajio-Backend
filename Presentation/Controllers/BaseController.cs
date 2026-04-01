using KPIBackend.Models;
using KPIBackend.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KPIBackend.Controllers
{
    /// <summary>
    /// Controlador base genérico que proporciona operaciones CRUD estándar
    /// para cualquier entidad que implemente <see cref="IEntity"/>.
    /// </summary>
    /// <typeparam name="TEntity">Tipo de entidad que manejará el controlador.</typeparam>
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController<TEntity> : ControllerBase where TEntity : class, IEntity
    {
        /// <summary>
        /// Repositorio genérico utilizado para acceder a los datos.
        /// </summary>
        protected readonly IBaseRepository<TEntity> _repository;

        /// <summary>
        ///Variable que ayuda a vincular los nombres de rol, facultad y período escolar para mostrar mensajes.
        /// </summary>
        protected virtual string EntityDisplayName => "registro";

        /// <summary>
        /// Constructor del controlador base.
        /// </summary>
        /// <param name="repository">Repositorio de la entidad.</param>
        public BaseController(IBaseRepository<TEntity> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Obtiene todos los registros de la entidad.
        /// </summary>
        /// <returns>Lista de entidades.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _repository.GetAllAsync();
            return Ok(entities);
        }
        /// <summary>
        /// Obtiene un registro específico mediante su identificador.
        /// </summary>
        /// <param name="id">Identificador único de la entidad.</param>
        /// <returns>Entidad encontrada o error 404.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);

            return entity == null ? NotFound($"{EntityDisplayName} no existe.") : Ok(entity);
        }

        /// <summary>
        /// Crea un nuevo registro en la base de datos.
        /// </summary>
        /// <param name="entity">Entidad a crear.</param>
        /// <returns>Entidad creada.</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TEntity entity)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Se determina el nombre amigable según el tipo de la clase
            string entityName = entity switch
            {
                Facultad => "facultad",
                Rol => "rol",
                PeriodoEscolar => "período escolar",
                _ => ""
            };

            if (entity is IUniqueName named)
            {
                var exists = await _repository.ExistsByNameAsync(named.Nombre);
                if (exists)
                    return Conflict($"Ya existe un registro con ese nombre de {entityName}.");
            }

            if (entity is IUniqueNumber groupNumber)
            {
                var exists = await _repository.ExistsByGroupNumberAsync(groupNumber.NumeroGrupo, groupNumber.DescripcionGrupo);
                if (exists)
                    return Conflict("Ya existe un registro con ese número de grupo de indicadores o con esa descripción del grupo.");
            }

            var created = await _repository.AddAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = GetId(created) }, created);
        }

        /// <summary>
        /// Actualiza un registro existente.
        /// </summary>
        /// <param name="id">Identificador de la entidad.</param>
        /// <param name="entity">Entidad con los datos actualizados.</param>
        /// <returns>Resultado de la operación.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] TEntity entity)
        {
            if (!IsSameEntity(id, entity)) return BadRequest("El ID no coincide.");

            if (await _repository.GetByIdAsync(id) == null) return NotFound($"{EntityDisplayName} no existe.");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            string entityName = entity switch
            {
                Facultad => "facultad",
                Rol => "rol",
                PeriodoEscolar => "período escolar",
                _ => ""
            };

            if (entity is IUniqueName named)
            {
                var exists = await _repository.ExistsByNameAsyncExceptId(named.Nombre, id);
                if (exists)
                    return Conflict($"Ya existe otro registro con ese nombre de {entityName}.");
            }

            if (entity is IUniqueNumber groupNumber)
            {
                var exists = await _repository.ExistsByGroupNumberAsyncExceptId(groupNumber.NumeroGrupo, id, groupNumber.DescripcionGrupo);
                if (exists)
                    return Conflict("Ya existe otro registro con ese número de grupo de indicadores con esa descripción del grupo.");
            }

            await _repository.UpdateAsync(entity);
            return NoContent();
        }

        /// <summary>
        /// Elimina un registro de la base de datos.
        /// </summary>
        /// <param name="id">Identificador de la entidad.</param>
        /// <returns>Resultado de la operación.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return NotFound($"{EntityDisplayName} no se encontró.");

            if (entity is IOwnedEntity owned)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var role = User.FindFirst(ClaimTypes.Role)?.Value;

                if (role != "Administrador" && owned.CreadorId.ToString() != userId)
                    return Forbid();
            }

            try
            {
                await _repository.DeleteAsync(id);
                return NoContent();
            }
            catch (InvalidOperationException ex) when (ex.Message == "FK_CONSTRAINT")
            {
                return Conflict("No se puede eliminar porque está siendo utilizado por otros registros.");
            }
        }

        private static Guid GetId(TEntity entity)
        {
            var prop = typeof(TEntity).GetProperty("Id");
            return prop != null ? (Guid)(prop.GetValue(entity) ?? Guid.Empty) : Guid.Empty;
        }

        private static bool IsSameEntity(Guid id, TEntity entity)
        {
            var prop = typeof(TEntity).GetProperty("Id");
            if (prop == null) return false;
            var entityId = (Guid)(prop.GetValue(entity) ?? Guid.Empty);
            return entityId == id;
        }
    }
}

