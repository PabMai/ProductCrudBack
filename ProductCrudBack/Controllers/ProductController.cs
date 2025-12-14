using Microsoft.AspNetCore.Mvc;
using ProductCrudBack.DTOs.Product;
using ProductCrudBack.Models;
using ProductCrudBack.Services;
using ProductCrudBack.Wrappers;

namespace ProductCrudBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        
        public ProductController(IProductService  productService)
        {
            _productService = productService;
        }
        
        // GET: api/<Product>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                IEnumerable<Product> products = await _productService.GetAllAsync();

                IEnumerable<ProductResponse> productsData = new ProductResponse().ToEnumerable(products);
            
                return Ok(ResponseResult<IEnumerable<ProductResponse>>.Success(productsData));
            }
            catch (Exception e)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError, 
                    ResponseResult<string>.Fail(e.Message)
                );
            }
        }

        // GET api/<Product>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                Product? product = await _productService.GetByIdAsync(id);

                if (product == null)
                {
                    return NotFound(ResponseResult<string>.Fail($"Product with id {id} not found"));
                }
                
                return Ok(ResponseResult<ProductResponse>.Success(new ProductResponse(product)));
            }
            catch (Exception e)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError, 
                    ResponseResult<string>.Fail(e.Message)
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
                {
                    var errors = await _productService.GetModelStateErrorsAsync(ModelState);
                    return BadRequest(
                            ResponseResult<Dictionary<string, IEnumerable<string>>>.Fail(errors)
                        );
                }
                
                Product product = new Product
                {
                    Name = dto.Name,
                    Price = dto.Price,
                    CategoryId = dto.CategoryId
                };

                await _productService.AddAsync(product);

                return Ok(ResponseResult<ProductResponse>.Success(new ProductResponse(product)));
            }
            catch (Exception e)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError, 
                    ResponseResult<string>.Fail(e.Message)
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
                {
                    var erros = await _productService.GetModelStateErrorsAsync(ModelState);
                    return BadRequest(
                        ResponseResult<Dictionary<string, IEnumerable<string>>>.Fail(erros)
                    );
                }
                
                Product? product = await _productService.GetByIdAsync(id);

                if (product == null)
                {
                    return NotFound(ResponseResult<string>.Fail($"Product with id {id} not found"));
                }

                product.Name = productUpdateDto.Name;
                product.Price = productUpdateDto.Price;
                product.CategoryId = productUpdateDto.CategoryId;

                await _productService.UpdateAsync(product);

                return Ok(ResponseResult<ProductResponse>.Success(new ProductResponse(product)));
            }
            catch (Exception e)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError, 
                    ResponseResult<string>.Fail(e.Message)
                );
            }
        }

        // DELETE api/<Product>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Product? product = await _productService.GetByIdAsync(id);

                if (product == null)
                {
                    return NotFound(ResponseResult<string>.Fail($"Product with id {id} not found"));
                }

                await _productService.DeleteAsync(product);

                return Ok(ResponseResult<ProductResponse?>.Success(null));
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
