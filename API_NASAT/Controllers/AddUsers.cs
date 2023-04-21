
using Microsoft.AspNetCore.Mvc;
using API_NASAT.Context;
using API_NASAT.Validate;
using API_NASAT.Models;

namespace API_NASAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddUsers : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ApplicationDbContext _context;

        public AddUsers(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost]
        public async Task<ActionResult<DbUsers>> PostUsersModels(AddUser userModels)
        {

            if (_context.Usuarios.Any(x => x.Username == userModels.Username.ToLower()))
            {
                return NotFound("Este usuario ya existe");

            }
            else
            {
                var Fecha = DateTime.Now;
                var pass = userModels.Password;
                var count = " Se requiere solo";

                bool Symbol = Chars.Symb(pass);
                bool Number = Chars.Num(pass);
                bool Mayus = Chars.MayusC(pass);
                bool Minus = Chars.MinusC(pass);

                var json = new DbUsers
                {
                    Id = 0,
                    Username = userModels.Username.ToLower(),
                    Fullname = userModels.Fullname.ToUpper(),
                    Dataset = Fecha.ToString(),
                    Typeuser = userModels.Typeuser,
                    Password = Crypto.Encriptar(pass)

                };

                if (pass.Length == 10 && Symbol && Number && Mayus && Minus)
                {
                    if (userModels.Verify == "1")
                    {
                        if (userModels.Token == Crypto.Encriptar(Crypto.Encriptar(userModels.key)))
                        {
                            _context.Usuarios.Add(json);
                            await _context.SaveChangesAsync();

                            return Ok(Crypto.Encriptar(Crypto.Encriptar(userModels.key)));
                        }
                        else
                        {
                            return NotFound("El código de verificación es incorrecto");
                        }
                    }
                    else
                    {

                        var codeKey = Code.Generate();
                        var dataEmail = _config.GetSection("SessionMail").Get<SessionMail>();

                        string code_verify = Crypto.Encriptar(codeKey);

                        if (Email.SendEmail(dataEmail.Address, dataEmail.Pass, codeKey, userModels.Username.ToLower()))
                        {
                            return Ok(Crypto.Encriptar(code_verify));
                        }
                        else
                        {
                            return NotFound("No se pudo enviar el código");
                        }

                    }


                }
                else
                {
                    if (!Number) count += " (2 Números)";
                    if (!Mayus) count += " (3 Mayúsculas)";
                    if (!Minus) count += " (3 Minúsculas)";
                    if (!Symbol) count += " (2 Símbolos)";
                    if (pass.Length != 10) count += " y 10 Caracteres en total";
                    return NotFound("La contraseña no cumple con los requisitos:" + count + ".");


                }

            }
        }

    }
}
