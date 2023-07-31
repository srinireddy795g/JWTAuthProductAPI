using JWTAuth.ProductAPI.Interface;
using Microsoft.EntityFrameworkCore;

namespace JWTAuth.ProductAPI.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            :base(options)
        {
        }

        public DbSet<Product> Product { get; set; } = default!;
        public DbSet<UserData> UserData { get; set; } = default!;  
    }
}
