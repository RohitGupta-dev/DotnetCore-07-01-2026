using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository_Pattern.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Repository_Pattern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration= configuration;
        }
        [HttpPost]
        public ActionResult Login(LoginDTO model)
        {
            LoginResponseDTO response = new() { UserName=model.UserName};
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if(model.UserName=="rohit" && model.Password == "123")
            {
                var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JWTSecret"));
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescription = new SecurityTokenDescriptor()
                {
                    Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                    {
                        //username
                        new Claim(ClaimTypes.Name,model.UserName),
                        //Role
                        new Claim(ClaimTypes.Role,"Admin"),

                    }),
                    Expires = DateTime.Now.AddHours(4),
                    SigningCredentials = new(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha512Signature)

                };
                var token = tokenHandler.CreateToken(tokenDescription);
                response.Token = tokenHandler.WriteToken(token);
            }
            else
            {
                return Ok("INvalid cred");
            }

            return Ok(response);
        }
    }
}
