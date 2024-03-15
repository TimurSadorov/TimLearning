using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TimLearning.Domain.Configurations.Options;

namespace TimLearning.Domain.Services.UserServices;

public class TokenGenerator : ITokenGenerator
{
    private const string CharChoicesForRefreshToken =
        "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    private const int RefreshTokenLength = 20;

    private readonly JwtSecretOptions _jwtSecretOptions;

    public TokenGenerator(IOptions<JwtSecretOptions> jwtSecretOptions)
    {
        _jwtSecretOptions = jwtSecretOptions.Value;
    }

    public string GenerateJwtToken(ClaimsIdentity identity, DateTime expires)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = identity,
            Expires = expires,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(_jwtSecretOptions.KeyInByte),
                SecurityAlgorithms.HmacSha512
            )
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        return RandomNumberGenerator.GetString(CharChoicesForRefreshToken, RefreshTokenLength);
    }
}
