using KPIBackend.Models;
using KPIBackend.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KPIBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController<TEntity> : ControllerBase where TEntity : class, IEntity
    {
        protected readonly IBaseRepository<TEntity> _repository;

        public BaseController(IBaseRepository<TEntity> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _repository.GetAllAsync();
            return Ok(entities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity == null ? NotFound($"No se encontró la entidad con ID {id}.") : Ok(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TEntity entity)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (entity is IUniqueName named)
            {
                var exists = await _repository.ExistsByNameAsync(named.Nombre);
                if (exists)
                    return Conflict("Ya existe un registro con ese nombre de facultad, período escolar o rol.");
            }

            if (entity is IUniqueNumber groupNumber)
            {
                var exists = await _repository.ExistsByGroupNumberAsync(groupNumber.numeroGrupo, groupNumber.DescripcionGrupo);
                if (exists)
                    return Conflict("Ya existe un registro con ese número de grupo de indicadores o con esa descripción del grupo.");
            }

            var created = await _repository.AddAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = GetId(created) }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] TEntity entity)
        {
            if (!IsSameEntity(id, entity)) return BadRequest("El ID no coincide.");

            if (await _repository.GetByIdAsync(id) == null) return NotFound($"No se encontró la entidad con ID {id}.");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (entity is IUniqueName named)
            {
                var exists = await _repository.ExistsByNameAsyncExceptId(named.Nombre, id);
                if (exists)
                    return Conflict("Ya existe otro registro con ese nombre de facultad, período escolar o rol.");
            }

            if (entity is IUniqueNumber groupNumber)
            {
                var exists = await _repository.ExistsByGroupNumberAsyncExceptId(groupNumber.numeroGrupo, id, groupNumber.DescripcionGrupo);
                if (exists)
                    return Conflict("Ya existe otro registro con ese número de grupo de indicadores con esa descripción del grupo.");
            }

            await _repository.UpdateAsync(entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return NotFound("No se encontró la entidad.");

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

