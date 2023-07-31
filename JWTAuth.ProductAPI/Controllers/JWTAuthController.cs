using JWTAuth.ProductAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTAuth.ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JWTAuthController : Controller
    {

        IConfiguration _configuration;

        public JWTAuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        [AllowAnonymous]
        [HttpPost]
        public IActionResult JWTAuth([FromBody] UserData user)
        {
            IActionResult result= Unauthorized();

            if (user != null)
            {
                if(user.UserName == "Admin" && user.Password == "admin123")
                {
                    var issuer = _configuration["Jwt:Issuer"];
                    var audience = _configuration["Jwt:Audience"];
                    var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

                    var siginingCredentails = new SigningCredentials(new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha512Signature);

                    var subject = new ClaimsIdentity(new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Email, user.UserName)
                    });

                    var expires = DateTime.UtcNow.AddMinutes(10);

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = subject,
                        Expires = expires,
                        Issuer = issuer,
                        Audience = audience,
                        SigningCredentials = siginingCredentails
                    };

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var jwtToken = tokenHandler.WriteToken(token);

                    return Ok(jwtToken);                   
                }
            }

            return result;
        }
    }
}
