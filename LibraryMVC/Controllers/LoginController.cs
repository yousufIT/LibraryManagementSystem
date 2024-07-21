using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LibraryMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Login = LibraryMVC.Models.Login;
namespace LibraryMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //this controller just have post for login actualy we can add get claims "for example"

    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public ActionResult login([FromBody] Login login)
        {
            var username = _configuration["Login:Username"];
            var password = _configuration["Login:Password"];
            if (login.Username == username && login.Password == password)
            {
                //so in this variable we are taking the sk from appsetting.json
                var secretkey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
                    _configuration["Authentivation:SecretKey"]));
                //and here we coding it by HS56
                var signingcred = new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256);
                //this creating the security token data like issuer .....
                var securityToken = new JwtSecurityToken(
                    _configuration["Authentivation:Issuer"],
                    _configuration["Authentivation:Audience"],
                    new List<Claim>(),
                    DateTime.UtcNow,
                    DateTime.UtcNow.AddDays(1),
                    signingcred
                );
                //and finally this give the token 
                var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
                return Ok(token);
            }
            else
                return Unauthorized();
        }
    }
}
