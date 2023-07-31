using JWTAuth.ProductAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuth.ProductAPI.Interface
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProducts();
        Task<Product?> GetProductById(int id);
        Task<ProductResponse> PutProduct(int id, Product product);
        Task<ProductResponse> PostProduct(Product product);
        Task<ProductResponse> DeleteProduct(int id);   
    }
}
