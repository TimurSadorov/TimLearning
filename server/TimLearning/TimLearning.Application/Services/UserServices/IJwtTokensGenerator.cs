using System.Security.Claims;

namespace TimLearning.Application.Services.UserServices;

public interface IJwtTokensGenerator
{
    Task<string> GenerateToken(ClaimsIdentity identity, TimeSpan lifeTime);
}
