//using Application.DTOs;
//using Application.Interfaces;
//using Microsoft.Extensions.Options;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Text;

//namespace Application.Services
//{
//    public class JwtService : IJwtService
//    {
//        private readonly JwtSettings _jwtSettings;

//        public JwtService(IOptions<JwtSettings> jwtSettings)
//        {
//            _jwtSettings = jwtSettings.Value;
//        }

//        public async Task<string> GerarJwt()
//        {
//            var tokenHandler = new JwtSecurityTokenHandler();
//            var key = Encoding.ASCII.GetBytes(_jwtSettings.Segredo);

//            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
//            {
//                Issuer = _jwtSettings.Emissor,
//                Audience = _jwtSettings.Audiencia,
//                Expires = DateTime.UtcNow.AddHours(_jwtSettings.ExpiracaoHoras),
//                NotBefore = DateTime.UtcNow,
//                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
//            });

//            var encodedToken = tokenHandler.WriteToken(token);

//            return encodedToken;
//        }
//    }
//}
