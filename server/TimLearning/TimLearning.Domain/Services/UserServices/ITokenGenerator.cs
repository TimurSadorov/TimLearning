using System.Security.Claims;

namespace TimLearning.Domain.Services.UserServices;

public interface ITokenGenerator
{
    string GenerateJwtToken(ClaimsIdentity identity, DateTime expires);
    string GenerateRefreshToken();
}
