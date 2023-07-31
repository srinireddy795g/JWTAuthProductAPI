using JWTAuth.ProductAPI.Interface;
using JWTAuth.ProductAPI.Models;
using JWTAuth.ProductAPI.Respository;
using JWTAuth.ProductAPI.Tests.Helpers;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using ProductWebAPI.Controllers;
using System.Threading.Tasks;

namespace JWTAuth.ProductAPI.Tests
{

    [TestFixture]
    public class TestProductController
    {
        //setup

        private readonly IProductRepository _repository;
        private DatabaseContext _context;
        private ILogger<IProductRepository> _logger;

        public TestProductController(IProductRepository repository, DatabaseContext context, ILogger<ProductRespository> logger)
        {
            _repository = repository;
            _context = context;
            _logger = logger;
        }

        [TestCase]
        public async void GetProductByIdReturnsOk()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            var dbContext = await Utilities.GetDatabaseContext();      

            // Act
            var response = _repository.GetAllProducts();
            _context = dbContext;

            //await _context.Product..toli.Product.ToListAsync();


            // Assert
            Assert.IsTrue(response.IsCompleted);
            //Assert.IsTrue(await dbContext.Product.FindAsync().. == 3);
            
        }
    }
}
