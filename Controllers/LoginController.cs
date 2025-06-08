using LoginAPI.BR;
using LoginAPI.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Unicode;

namespace LoginAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly LoginService _loginService;
        private readonly IConfiguration _configurationBuilder;
        public LoginController(LoginService loginService, IConfiguration configurationBuilder) {
        
        _loginService= loginService;
            _configurationBuilder= configurationBuilder;
        
        }


        [HttpPost("Login")]
        public IActionResult Login([FromBody]Login login)
        {

            var user =  _loginService.GetUserAsync().Result;
            var filteruser = user.Where(user => user.Name == login.Username && user.Password == login.Password).FirstOrDefault();
            if (user != null && filteruser!=null)
            {
                var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub,_configurationBuilder["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim("Username",filteruser.Name.ToString()),
                    new Claim("Password",filteruser.Password.ToString()),

                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configurationBuilder["Jwt:Key"]));
                var signin = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(_configurationBuilder["Jwt:Issuer"], _configurationBuilder["Jwt:Audience"],claims,expires:DateTime.UtcNow.AddMinutes(60),signingCredentials:signin);
            string tokenvalue=new JwtSecurityTokenHandler().WriteToken(token);

                Response.Cookies.Append("jwt", tokenvalue , new CookieOptions
                {
                    HttpOnly = true,
                    Secure=true,
                    SameSite=SameSiteMode.Strict
                    

                }); ;
                return Ok(new { Token = tokenvalue, User = user });

            }
            return NoContent();
        }
    }
}
