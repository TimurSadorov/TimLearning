using System.Security.Claims;
using System.Security.Cryptography;
using TimLearning.Application.Auth.Claim;
using TimLearning.Application.Services.UserServices.Dto;

namespace TimLearning.Application.Services.UserServices;

public class UserTokenGenerator : IUserTokenGenerator
{
    private const string CharChoicesForRefreshToken =
        "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    private const int RefreshTokenLength = 20;

    private static readonly TimeSpan JwtTokenLifetime = TimeSpan.FromMinutes(30);

    private readonly IJwtTokensGenerator _jwtTokensGenerator;

    public UserTokenGenerator(IJwtTokensGenerator jwtTokensGenerator)
    {
        _jwtTokensGenerator = jwtTokensGenerator;
    }

    public Task<AuthTokensDto> GenerateTokens(UserClaimsDto userClaims)
    {
        var identity = GetIdentity(userClaims);

        return GenerateTokens(identity);
    }

    private static ClaimsIdentity GetIdentity(UserClaimsDto user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, user.Email),
            new(ExtendedClaimTypes.Id, user.Id.ToString())
        };
        return new ClaimsIdentity(claims);
    }

    private async Task<AuthTokensDto> GenerateTokens(ClaimsIdentity identity)
    {
        return new AuthTokensDto(
            await _jwtTokensGenerator.GenerateToken(identity, JwtTokenLifetime),
            GenerateRefreshToken()
        );
    }

    private string GenerateRefreshToken()
    {
        return RandomNumberGenerator.GetString(CharChoicesForRefreshToken, RefreshTokenLength);
    }
}
