using Math4FunBackedn.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Asn1.Ocsp;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Math4FunBackedn.Repositories.TokenRepo
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration _config;
        public TokenRepository(IConfiguration configuration)
        {
            _config = configuration;
        }
        public async Task<string> DecodeToken(string iToken)
        {
            string token = iToken.Substring("Bearer ".Length).Trim(); ;
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken securityToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
            IEnumerable<Claim> claims = securityToken.Claims;
            string userId = claims.First(c => c.Type == "userId").Value;
            Console.WriteLine("==== User Id ====", userId);
            return "1";
        }
        public async Task<string> GenerateToken(User user)
        {
            var claims = new[]
                {
                    new Claim("userId", user.Id.ToString()),
                    new Claim("email", user.Email.ToString())
                };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]));
            var token = new JwtSecurityToken(
                issuer: _config["JwtSettings:Issuer"],
                audience: _config["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8), // Token expiration time
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
