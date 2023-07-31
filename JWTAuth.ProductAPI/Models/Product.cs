using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JWTAuth.ProductAPI.Models
{
    public class Product
    {
        [Key, Column(Order = 0)]
        public int Id { get; set; }

        [Column(Order = 1)]
        public string? Name { get; set; }

        [Column(Order = 2)]
        public string? Description { get; set; }

        [Column(TypeName = "decimal(18,2)", Order = 3)]
        public decimal Price { get; set; }       
    }
}
