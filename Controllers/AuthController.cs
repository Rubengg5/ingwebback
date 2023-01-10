using Google.Apis.Auth;
using WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using B3serverREST.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Controller]
    public class AuthController : Controller
    {
        private readonly UsuariosService _usuariosService;
        private readonly AppSettings _applicationSettings;

        public AuthController(IOptions<AppSettings> _applicationSettings, UsuariosService usuariosService)
        {
            this._applicationSettings = _applicationSettings.Value;
            this._usuariosService = usuariosService;
        }


        /*[HttpPost("Login")]
        public IActionResult Login([FromBody] Login model)
        {
            var user = UserList.Where(x => x.UserName == model.UserName).FirstOrDefault();

            if (user == null)
            {
                return BadRequest("Username Or Password Was Invalid");
            }

            var match = CheckPassword(model.Password, user);

            if (!match)
            {
                return BadRequest("Username Or Password Was Invalid");
            }
            //JWTGenerator(user);
            return Ok(JWTGenerator(user));

        }*/

        public dynamic JWTGenerator(Usuario user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("some token secret message");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.UserName), new Claim(ClaimTypes.Role, user.Role),
                        new Claim(ClaimTypes.DateOfBirth, user.BirthDay)}),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encrypterToken = tokenHandler.WriteToken(token);

            HttpContext.Response.Cookies.Append("token", encrypterToken,
                 new CookieOptions
                 {
                     Expires = DateTime.Now.AddDays(7),
                     HttpOnly = true,
                     Secure = true,
                     IsEssential = true,
                     SameSite = SameSiteMode.None
                 });

            return new { token = encrypterToken, id = user.Id };
        }


        [HttpPost("LoginWithGoogle")]
        public async Task<IActionResult> LoginWithGoogle([FromBody] string credential)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { this._applicationSettings.GoogleClientId }
            };


            var payload = await GoogleJsonWebSignature.ValidateAsync(credential);
            var user = _usuariosService.GetUsuarioByEmail(payload.Email).Result;
            Console.WriteLine(payload.Email);

            if (user != null)
            {
                Console.WriteLine("Inicia sesion", user.Email);
                return Ok(JWTGenerator(user));
            }
            else
            {
                Usuario userNuevo = new Usuario();
                userNuevo.UserName = payload.Name;
                userNuevo.Email = payload.Email;
                userNuevo.BirthDay = "-1";
                userNuevo.Role = "user";
                _ = _usuariosService.CreateUsuario(userNuevo);
                Console.WriteLine("Registro", userNuevo);
                return Ok(JWTGenerator(userNuevo));
            }
        }

        private bool CheckPassword(string password, User user)
        {
            bool result;

            using (HMACSHA512? hmac = new HMACSHA512(user.PasswordSalt))
            {
                var compute = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                result = compute.SequenceEqual(user.PasswordHash);
            }

            return result;
        }

        /*[HttpPost("Register")]
        public IActionResult Register([FromBody] Register model)
        {
            var user = new User { UserName = model.UserName, Role = model.Role, BirthDay = model.BirthDay };

            if (model.ConfirmPassword == model.Password)
            {
                using (HMACSHA512? hmac = new HMACSHA512())
                {
                    user.PasswordSalt = hmac.Key;
                    user.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(model.Password));
                }
            }
            else
            {
                return BadRequest("Passwords Dont Match");
            }

            UserList.Add(user);

            return Ok(user);
        }*/
    }
}
