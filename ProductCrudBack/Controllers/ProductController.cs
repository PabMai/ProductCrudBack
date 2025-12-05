using Microsoft.AspNetCore.Mvc;
using ProductCrudBack.DTOs.Product;
using ProductCrudBack.Models;
using ProductCrudBack.Repositories;

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
            
                return Ok(products);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
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
                    return NotFound();
                }

                return Ok(product);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // POST api/<Product>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductCreateDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                Product product = new Product
                {
                    Name = dto.Name,
                    Price = dto.Price,
                    CategoryId = dto.CategoryId
                };

                await _productRepository.AddAsync(product);

                return Ok(product);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // PUT api/<Product>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProductUpdateDto productUpdateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                Product? product = await _productRepository.GetByIdAsync(id);

                if (product == null)
                {
                    return NotFound();
                }

                product.Name = productUpdateDto.Name;
                product.Price = productUpdateDto.Price;
                product.CategoryId = productUpdateDto.CategoryId;

                await _productRepository.UpdateAsync(product);

                return Ok(product);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
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
                    return NotFound();
                }

                await _productRepository.DeleteAsync(product);

                return Ok(product);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
