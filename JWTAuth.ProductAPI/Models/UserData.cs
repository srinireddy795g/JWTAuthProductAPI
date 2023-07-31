using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JWTAuth.ProductAPI.Models
{
    public class UserData
    {
        [Key, Column(Order =0)]
        public int Id { get; set; }

        [Column(Order =1)]
        public string? Name { get; set; }

        [Column(Order =2)]
        public string? UserName { get; set; }

        [Column(Order =3)]
        public string? Password { get; set; }

        [Column(Order = 4)]
        public string? Email { get; set; }

        //public UserData(string? username, string? password)
        //{
        //    UserName = username;
        //    Password = password;
        //}     
    }
}
