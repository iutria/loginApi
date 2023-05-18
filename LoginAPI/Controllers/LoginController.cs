using LoginAPI.Models;
using LoginAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LoginAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly ContextBD _context;
        public LoginController(IConfiguration config, ContextBD context)
        {
            _configuration = config;
            _context = context;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<Usuario>> Post(LoginInputModel loginInput)
        {

            if (loginInput != null && loginInput.Correo != null && loginInput.Clave != null)
            {
                var user = await GetUser(loginInput.Correo, loginInput.Clave);
                if (user != null)
                {
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Id", user.Id.ToString()),
                        new Claim("Correo", user.Correo),
                        new Claim("Estado", user.Estado.ToString()),
                        new Claim (ClaimTypes.Role, user.Rol.ToString())
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest("error personalizado");
            }
        }
        private async Task<Usuario> GetUser(string correo, string clave)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == correo && u.Clave == clave);
        }
    }
}
