using Microsoft.AspNetCore.Mvc;
using ProductCrudBack.DTOs.Product;
using ProductCrudBack.Models;
using ProductCrudBack.Repositories;
using ProductCrudBack.Wrappers;

namespace ProductCrudBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        
        public ProductController(IProductRepository  productRepository)
        {
            _productRepository = productRepository;
        }
        
        // GET: api/<Product>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                IEnumerable<Product> products = await _productRepository.GetAllAsync();
            
                return Ok(ResponseResult.ResponseValue(products));
            }
            catch (Exception e)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError, 
                    ResponseResult.ResponseError(e.Message)
                );
            }
        }

        // GET api/<Product>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                Product? product = await _productRepository.GetByIdAsync(id);

                if (product == null)
                {
                    return NotFound(ResponseResult.ResponseError($"Product with id {id} not found"));
                }

                return Ok(product);
            }
            catch (Exception e)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError, 
                    ResponseResult.ResponseError(e.Message)
                );
            }
        }

        // POST api/<Product>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductCreateDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ResponseResult.ResponseError(ModelState
                        .Where(x => x.Value.Errors.Count > 0)
                        .ToDictionary(x => x.Key, 
                            x => x.Value.Errors.Select(x => x.ErrorMessage))
                    ));

                Product product = new Product
                {
                    Name = dto.Name,
                    Price = dto.Price,
                    CategoryId = dto.CategoryId
                };

                await _productRepository.AddAsync(product);

                return Ok(ResponseResult.ResponseValue(product));
            }
            catch (Exception e)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError, 
                    ResponseResult.ResponseError(e.Message)
                );
            }
        }

        // PUT api/<Product>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProductUpdateDto productUpdateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ResponseResult.ResponseError(ModelState
                        .Where(x => x.Value.Errors.Count > 0)
                        .ToDictionary(x => x.Key, 
                            x => x.Value.Errors.Select(x => x.ErrorMessage))
                    ));

                Product? product = await _productRepository.GetByIdAsync(id);

                if (product == null)
                {
                    return NotFound(ResponseResult.ResponseError($"Product with id {id} not found"));
                }

                product.Name = productUpdateDto.Name;
                product.Price = productUpdateDto.Price;
                product.CategoryId = productUpdateDto.CategoryId;

                await _productRepository.UpdateAsync(product);

                return Ok(ResponseResult.ResponseValue(product));
            }
            catch (Exception e)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError, 
                    ResponseResult.ResponseError(e.Message)
                );
            }
        }

        // DELETE api/<Product>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Product? product = await _productRepository.GetByIdAsync(id);

                if (product == null)
                {
                    return NotFound(ResponseResult.ResponseError($"Product with id {id} not found"));
                }

                await _productRepository.DeleteAsync(product);

                return Ok(ResponseResult.ResponseValue(product));
            }
            catch (Exception e)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError, 
                    ResponseResult.ResponseError(e.Message)
                );
            }
        }
    }
}
