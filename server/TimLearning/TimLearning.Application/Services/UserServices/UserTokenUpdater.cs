using Microsoft.EntityFrameworkCore;
using TimLearning.Application.Services.UserServices.Dto;
using TimLearning.Application.Services.UserServices.Mappers;
using TimLearning.Application.Specifications.Dynamic.Users;
using TimLearning.Infrastructure.Interfaces.Db;
using TimLearning.Infrastructure.Interfaces.Providers.Clock;

namespace TimLearning.Application.Services.UserServices;

public class UserTokenUpdater : IUserTokenUpdater
{
    private static readonly TimeSpan RefreshTokenLifeTime = TimeSpan.FromDays(30);

    private readonly IAppDbContext _db;
    private readonly IUserTokenGenerator _userTokenGenerator;
    private readonly IDateTimeProvider _dateTimeProvider;

    public UserTokenUpdater(
        IAppDbContext db,
        IUserTokenGenerator userTokenGenerator,
        IDateTimeProvider dateTimeProvider
    )
    {
        _db = db;
        _userTokenGenerator = userTokenGenerator;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<AuthTokensDto> UpdateUserTokens(string email)
    {
        var user = await _db.Users
            .Include(u => u.Roles)
            .SingleAsync(new UserByEmailSpecification(email));

        var tokens = await _userTokenGenerator.GenerateTokens(user.ToUserClaimsDto(user.Roles));

        var now = await _dateTimeProvider.GetUtcNow();
        user.RefreshToken = tokens.RefreshToken;
        user.RefreshTokenExpireAt = now.Add(RefreshTokenLifeTime);

        await _db.SaveChangesAsync();

        return tokens;
    }
}
