using Microsoft.EntityFrameworkCore;
using TimLearning.Domain.Services.UserServices;
using TimLearning.Domain.Services.UserServices.Dto;
using TimLearning.Infrastructure.Interfaces.Db;
using TimLearning.Infrastructure.Interfaces.Providers.Clock;

namespace TimLearning.Application.Services.UserServices;

public class UserTokenUpdater : IUserTokenUpdater
{
    private readonly IAppDbContext _db;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUserTokenService _userTokenService;

    public UserTokenUpdater(
        IAppDbContext db,
        IDateTimeProvider dateTimeProvider,
        IUserTokenService userTokenService
    )
    {
        _db = db;
        _dateTimeProvider = dateTimeProvider;
        _userTokenService = userTokenService;
    }

    public async Task<AuthTokensDto> UpdateUserTokens(string email)
    {
        var user = await _db.Users.Include(u => u.Roles).FirstAsync(u => u.Email == email);

        var tokens = _userTokenService.UpdateTokens(
            user,
            user.Roles.Select(r => r.Type).ToList(),
            await _dateTimeProvider.GetUtcNow()
        );

        await _db.SaveChangesAsync();

        return tokens;
    }
}
