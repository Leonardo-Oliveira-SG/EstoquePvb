using Application.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{
    [ApiController]
    [Route("api/conta")]
    public class AuthController : Controller
    {
        
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtSettings _jwtSettings;

        public AuthController(SignInManager<IdentityUser> signInManager, 
                              UserManager<IdentityUser> userManager, 
                              IOptions<JwtSettings> jwtSettings)
        {
            
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("registrar")]
        public async Task<ActionResult> Registrar(RegistrarNovoUsuarioDto registrarUsuario)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var user = new IdentityUser
            {
                UserName = registrarUsuario.Email,
                Email = registrarUsuario.Email,
                EmailConfirmed = true,
            };

            var result = await _userManager.CreateAsync(user, registrarUsuario.Password!);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);

                return Ok(await GerarJwt(user.Email!));            
            }


            return Problem("Falha ao registrar o usuário");
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginUsuarioDto logarUsuario)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var result = await _signInManager.PasswordSignInAsync(logarUsuario.Email, logarUsuario.Password, false, true);

            if (result.Succeeded)
            {
                return Ok(await GerarJwt(logarUsuario.Email));
            }

            return Problem("Usuário ou senha incorretos");
        }

        private async Task<string> GerarJwt(string email)
        {
            var user = await _userManager.FindByNameAsync(email);
            var roles = await _userManager.GetRolesAsync(user!);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user!.UserName!)
            };

            // Adicionar roles com claims
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Segredo!);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _jwtSettings.Emissor,
                Audience = _jwtSettings.Audiencia,
                NotBefore = DateTime.UtcNow, // Define o momento em que o token começa a ser válido
                Expires = DateTime.UtcNow.AddHours(_jwtSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            return encodedToken;
        }

    }
}
