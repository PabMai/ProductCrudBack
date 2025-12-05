using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductCrudBack.DTOs.Category;
using ProductCrudBack.Models;
using ProductCrudBack.Repositories;

namespace ProductCrudBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // GET: api/<CategoryController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                IEnumerable<Category> categories = await _categoryRepository.GetAllAsync();
                
                return Ok(categories);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                Category? category = await _categoryRepository.GetByIdAsync(id);

                if (category == null)
                {
                    return NotFound();
                }
                
                return Ok(category);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // POST api/<CategoryController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CategoryCreateDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Category category = new Category
                {
                    Name = dto.Name
                };
                
                await _categoryRepository.AddAsync(category);
                
                return Ok(category);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CategoryUpdateDto categoryUpdateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                
                Category? category = await _categoryRepository.GetByIdAsync(id);

                if (category == null)
                {
                    return NotFound();
                }
                
                category.Name = categoryUpdateDto.Name;
                
                await _categoryRepository.UpdateAsync(category);
                
                return Ok(category);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Category? category = await _categoryRepository.GetByIdAsync(id);

                if (category == null)
                {
                    return NotFound();
                }
                
                await _categoryRepository.DeleteAsync(category);
                
                return Ok(category);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
