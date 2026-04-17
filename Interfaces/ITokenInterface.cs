using System.Security.Claims;

namespace WebAPI_React.Interfaces
{
    public interface ITokenInterface
    {
        string GenerateToken(string userName, string role);
        ClaimsPrincipal GetClaimsPrincipalToken(string token);
        string GetRefreshToken();
    }
}
