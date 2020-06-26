using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SurveyPortal.Data;
using SurveyPortal.Data.Entities;

namespace SurveyPortal.Controllers
{
    public class CrudController<TEntity, TUpdateRequest, TCreateRequest> : Controller
        where TEntity : class, IEntity, new()
        where TUpdateRequest : class, new()
        where TCreateRequest : class, new()
    {
        private readonly IGenericCrudRepository<TEntity, TUpdateRequest, TCreateRequest> _repo;

        public CrudController(IGenericCrudRepository<TEntity, TUpdateRequest, TCreateRequest> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public List<TEntity> GetAll()
        {
            return _repo.GetAll();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                return Ok(_repo.GetById(id));
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] TCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = _repo.Create(request);

            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] TUpdateRequest request)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repo.Update(id, request);
                    return Ok();
                }
                catch (InvalidOperationException)
                {
                    return BadRequest();
                }
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _repo.Delete(id);

                return NoContent();
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
