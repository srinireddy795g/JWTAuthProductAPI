using JWTAuth.ProductAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace JWTAuth.ProductAPI.Tests.Helpers
{
    public static class Utilities
    {
        public static async Task<DatabaseContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(databaseName: "AssignmentDatabase")
            .Options;

            var dbContext = new DatabaseContext(options);
            dbContext.Database.EnsureCreated();

            if (await dbContext.UserData.CountAsync() <= 0)
            {
                for (int i = 1; i < 1; i++)
                {
                    dbContext.UserData.Add(new UserData()
                    {
                        Id = i,
                        Name = "Srini Gaddam",
                        UserName = "Admin",
                        Password = "admin123",
                        Email = "Srini.Gaddam@gmail.com"
                    });

                    await dbContext.SaveChangesAsync();
                }

            }
            if (await dbContext.Product.CountAsync() <= 0)
            {
                dbContext.Product.Add(new Product { Id = 1, Name = "iPhone", Description = "smartphone by Apple", Price = 860 });
                dbContext.Product.Add(new Product { Id = 2, Name = "iPad", Description = "tab by Apple", Price = 760 });
                dbContext.Product.Add(new Product { Id = 3, Name = "iPod", Description = "music system by Apple", Price = 260 });
                dbContext.Product.Add(new Product { Id = 4, Name = "iWatch", Description = "smartwatch by Apple", Price = 360 });
                await dbContext.SaveChangesAsync();
            }

            return dbContext;
        } 
    }
}
