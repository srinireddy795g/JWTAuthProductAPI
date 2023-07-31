using JWTAuth.ProductAPI.Interface;
using JWTAuth.ProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace JWTAuth.ProductAPI.Respository
{
    public class ProductRespository : IProductRepository
    {
        private readonly DatabaseContext _context = new();
        private readonly ILogger<ProductRespository> _logger;   

        public ProductRespository(DatabaseContext context, ILogger<ProductRespository> logger)
        {
            _context = context;
            _logger = logger;
        }        

        public async Task<ProductResponse> DeleteProduct(int id)
        {
            // Create new product response object that we can return to the requesting method
            ProductResponse response = new();
            try
            {
                Product? product = await _context.Product.FindAsync(id);

                if (product != null)
                {
                    _context.Product.Remove(product);
                    await _context.SaveChangesAsync();
                    response.IsSuccess = true;
                    response.Message = $"Product with Id {id} deleted successfully";
                    _logger.LogInformation($"Product with Id {id} deleted successfully");
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = $"Product with Id {id} not found";
                    _logger.LogInformation($"Product with Id {id} not found");
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess=false;
                response.Message = $"An error occurred while deleting product " + ex.Message;
                _logger.LogError($"An error occurred while deleting product " + ex.Message);
            }

            return response;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            var products = new List<Product>();

            try
            {
                if (_context.Product == null)
                {
                    _logger.LogInformation($"Product is null");
                }
                else
                {
                    products = await _context.Product.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while fetching product " + ex.Message);
            }

            return products;
        }

        public async Task<Product?> GetProductById(int id)
        {
            var product = new Product();
            
            try
            {
                if (_context.Product == null)
                {
                    _logger.LogInformation($"Product is null");
                }

#pragma warning disable CS8602 // Dereference of a possibly null reference.
                product = await _context.Product.FindAsync(id);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

                if (product == null)
                {
                    return null;
                }
               
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while fetching product " + ex.Message);
            }

            return product;
        }

        public async Task<ProductResponse> PostProduct(Product product)
        {
            // Create new product response object that we can return to the requesting method
            ProductResponse response = new();

            try
            {
                await _context.Product.AddAsync(product);
                await _context.SaveChangesAsync();
                response.IsSuccess = true;
                response.Message = $"Product added successfully";
                _logger.LogInformation($"Product added successfully");
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"An error occurred while adding product " + ex.Message;
                _logger.LogError($"An error occurred while adding product " + ex.Message);
            }

            return response;
        }

        public async Task<ProductResponse> PutProduct(int id, Product product)
        {
            // Create new product response object that we can return to the requesting method
            ProductResponse response = new();

            try
            {
                _context.Product.Update(product);
                await _context.SaveChangesAsync();
                response.IsSuccess=true;
                response.Message = $"Product with Id {id} is updated successfully";
            }
            catch (Exception ex)
            {
                if (!ProductExists(id))
                {
                    response.IsSuccess = false;
                    response.Message = $"Product with provided Id {id} is not found";
                    return response;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = $"An error occured {ex.Message} while updating product with provided Id {id}";
                    _logger.LogError($"An error occurred {ex.Message} while updating product with Id {id}");
                }
            }

            return response;
        }
        private bool ProductExists(int id)
        {
            return (_context.Product?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
