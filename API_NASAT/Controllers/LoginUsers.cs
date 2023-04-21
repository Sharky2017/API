using API_NASAT.Context;
using API_NASAT.Models;
using API_NASAT.Validate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_NASAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginUsers : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ApplicationDbContext _context;

        public LoginUsers(IConfiguration config, ApplicationDbContext context)
        {
            _config = config;
            _context = context;
        }





        [HttpGet]
        public async Task<ActionResult<IEnumerable<QueryUsers>>> GetUsuarios()
        {
            var listUsers = await _context.Usuarios.ToListAsync();
            return Ok(listUsers);
        }

        [HttpPost]
        public IActionResult Login(LoginUser userLogin)
        {

            if (_context.Usuarios.Any(x => x.Username == userLogin.Username.ToLower()))
            {
                var user = Authenticate(userLogin);

                if (user != null)
                {
                    // Crear el token

                    var token = Generate(user);

                    return Ok(token);
                }

                return NotFound("La contraseña es incorrecta");

            }
            else
            {

                return NotFound("Este usuario no existe");

            }

        }

        private DbUsers? Authenticate(LoginUser userLogin)
        {
            var UserHere = _context.Usuarios.FirstOrDefault(user => user.Username.ToLower() == userLogin.Username.ToLower()
                   && user.Password == Crypto.Encriptar(userLogin.Password));

            if (UserHere != null)
            {
                return UserHere;
            }

            return null;
        }

        private string Generate(DbUsers user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Email, user.Username),
                new Claim(ClaimTypes.GivenName, user.Fullname),
                new Claim(ClaimTypes.Surname, user.Fullname),
                new Claim(ClaimTypes.Role, user.Typeuser),
            };


            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(240),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
