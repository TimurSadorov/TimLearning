using System.Security.Claims;
using TimLearning.Domain.Auth.Claim;
using TimLearning.Domain.Entities;
using TimLearning.Domain.Entities.Enums;
using TimLearning.Domain.Services.UserServices.Dto;

namespace TimLearning.Domain.Services.UserServices;

public class UserTokenService : IUserTokenService
{
    private static readonly TimeSpan JwtTokenLifetime = TimeSpan.FromMinutes(5);
    private static readonly TimeSpan RefreshTokenLifetime = TimeSpan.FromDays(30);

    private readonly ITokenGenerator _tokenGenerator;

    public UserTokenService(ITokenGenerator tokenGenerator)
    {
        _tokenGenerator = tokenGenerator;
    }

    public AuthTokensDto UpdateTokens(User user, List<UserRoleType> userRoles, DateTimeOffset now)
    {
        var identity = GetIdentity(user, userRoles);
        var tokens = GenerateTokens(identity, now);

        user.RefreshToken = tokens.RefreshToken;
        user.RefreshTokenExpireAt = now + RefreshTokenLifetime;

        return tokens;
    }

    private static ClaimsIdentity GetIdentity(User user, List<UserRoleType> userRoles)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, user.Email),
            new(ExtendedClaimTypes.Id, user.Id.ToString())
        };
        claims.AddRange(userRoles.Select(r => new Claim(ClaimTypes.Role, r.ToString())));

        return new ClaimsIdentity(claims);
    }

    private AuthTokensDto GenerateTokens(ClaimsIdentity identity, DateTimeOffset now)
    {
        return new AuthTokensDto(
            _tokenGenerator.GenerateJwtToken(identity, now.UtcDateTime + JwtTokenLifetime),
            _tokenGenerator.GenerateRefreshToken()
        );
    }
}
