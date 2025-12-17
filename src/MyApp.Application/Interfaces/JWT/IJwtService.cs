using MyApp.Domain.Abstractions;
using System.Security.Claims;

namespace MyApp.Application.Interfaces.Jwt
{
    public interface IJwtService
    {
        string GenerateAccessToken(IAppUserReference user, IList<string> roles);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);

    }

}
