using Microsoft.AspNetCore.Mvc;
using WebAPI.Dto;
using WebAPI.Entity;
using WebAPI.Repositories;

namespace WebAPI.Controllers
{
    public abstract class BaseController<T, TRequestDto> : ControllerBase
        where T : class, IEntity
        where TRequestDto : class, IRequestDto<T>
    {
        protected readonly IRepository<T> _repository;

        protected BaseController(IRepository<T> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public virtual IActionResult GetAll()
        {
            var entities = _repository.GetAll();
            return Ok(entities);
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetById(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create(TRequestDto requestDto)
        {
            var entity = requestDto.ToEntity();
            await _repository.AddAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
        }

        [HttpPut("{id}")]
        public virtual IActionResult Update(int id, TRequestDto requestDto)
        {
            var entity = requestDto.ToEntity();
            entity.Id = id;
            if (!_repository.Query().Contains(entity))
            {
                return NotFound();
            }

            _repository.Update(entity);

            return Ok(entity);
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            _repository.Delete(entity);
            return Ok(entity);
        }
    }
}
