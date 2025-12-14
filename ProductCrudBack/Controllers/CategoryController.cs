using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductCrudBack.DTOs.Category;
using ProductCrudBack.Models;
using ProductCrudBack.Services;
using ProductCrudBack.Wrappers;

namespace ProductCrudBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/<CategoryController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                IEnumerable<Category> categories = await _categoryService.GetAllAsync();
                
                IEnumerable<CategoryResponse> categoriesData = new CategoryResponse().ToEnumerable(categories);
                
                return Ok(ResponseResult<IEnumerable<CategoryResponse>>.Success(categoriesData));
            }
            catch (Exception e)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError, 
                    ResponseResult<string>.Fail(e.Message)
                );
            }
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                Category? category = await _categoryService.GetByIdAsync(id);

                if (category == null)
                {
                    return NotFound(ResponseResult<string>.Fail($"Category with id {id} not found"));
                }
                
                return Ok(ResponseResult<CategoryResponse>.Success(new CategoryResponse(category)));
            }
            catch (Exception e)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError, 
                    ResponseResult<string>.Fail(e.Message)
                );
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
                    var errors = await _categoryService.GetModelStateErrorsAsync(ModelState);
                    
                    return BadRequest(
                        ResponseResult<Dictionary<string, IEnumerable<string>>>.Fail(errors)
                    );
                }

                Category category = new Category
                {
                    Name = dto.Name
                };
                
                await _categoryService.AddAsync(category);
                
                return Ok(ResponseResult<CategoryResponse>.Success(new CategoryResponse(category)));
            }
            catch (Exception e)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError, 
                    ResponseResult<string>.Fail(e.Message)
                    );
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
                    var errors = await _categoryService.GetModelStateErrorsAsync(ModelState);
                    
                    return BadRequest(
                        ResponseResult<Dictionary<string, IEnumerable<string>>>.Fail(errors)
                    );
                }
                
                Category? category = await _categoryService.GetByIdAsync(id);

                if (category == null)
                {
                    return NotFound(ResponseResult<string>.Fail($"Category with id {id} not found"));
                }
                
                category.Name = categoryUpdateDto.Name;
                
                await _categoryService.UpdateAsync(category);
                
                return Ok(ResponseResult<CategoryResponse>.Success(new CategoryResponse(category)));
            }
            catch (Exception e)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError, 
                    ResponseResult<string>.Fail(e.Message)
                );
            }
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Category? category = await _categoryService.GetByIdAsync(id);

                if (category == null)
                {
                    return NotFound(ResponseResult<string>.Fail($"Category with id {id} not found"));
                }
                
                await _categoryService.DeleteAsync(category);
                
                return Ok(ResponseResult<CategoryResponse?>.Success(null));
            }
            catch (Exception e)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError, 
                    ResponseResult<string>.Fail(e.Message)
                );
            }
        }
    }
}
