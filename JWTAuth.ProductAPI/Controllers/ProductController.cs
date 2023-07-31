using JWTAuth.ProductAPI.Interface;
using JWTAuth.ProductAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProductWebAPI.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private DatabaseContext _context;
        private ILogger<ProductController> _logger;

        public ProductController(IProductRepository productRepository, DatabaseContext context, ILogger<ProductController> logger)
        {
            _productRepository = productRepository;
            _context = context;
            _logger = logger;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        {
            if (_context.Product == null)
            {
                return NotFound();
            }
            return await _productRepository.GetAllProducts();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            if (_context.Product == null)
            {
                return NotFound();
            }
            var product = await _productRepository.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5  
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {               
                await _productRepository.PutProduct(id, product);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(product);
        }

        // POST: api/Products  
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            if (_context.Product == null)
            {
                return Problem("Entity set 'DatabaseContext.Product'  is null.");
            }

            try
            {
                var response = await _productRepository.PostProduct(product);

                if(!response.IsSuccess)
                {
                    _logger.LogError($"An error occured {response.Message} while saving data");
                    throw new Exception(response.Message);
                    
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError($"An error occured {ex.Message} while saving data");
                throw ex;
            }

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (_context.Product == null)
            {
                return NotFound();
            }
            var product = await _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            
            await _productRepository.DeleteProduct(id);

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return (_context.Product?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
