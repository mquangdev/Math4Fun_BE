using Math4FunBackedn.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace Math4FunBackedn.Repositories.TokenRepo
{
    public interface ITokenRepository
    {
        Task<string> GenerateToken(User user);
        Task<Guid> DecodeToken(string token);
    }
}
