using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TimLearning.Application.Configurations.Options;
using TimLearning.Infrastructure.Interfaces.Providers.Clock;

namespace TimLearning.Application.Services.UserServices;

public class JwtTokenGenerator : IJwtTokensGenerator
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly JwtSecretOptions _jwtSecretOptions;

    public JwtTokenGenerator(
        IDateTimeProvider dateTimeProvider,
        IOptions<JwtSecretOptions> jwtSecretOptions
    )
    {
        _dateTimeProvider = dateTimeProvider;
        _jwtSecretOptions = jwtSecretOptions.Value;
    }

    public async Task<string> GenerateToken(ClaimsIdentity identity, TimeSpan lifeTime)
    {
        var now = await _dateTimeProvider.GetDateTimeUtcNow();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = identity,
            Expires = now.Add(lifeTime),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(_jwtSecretOptions.KeyInByte),
                SecurityAlgorithms.HmacSha512
            )
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
