using Microsoft.EntityFrameworkCore;
using TimLearning.Application.UseCases.Users.Dto;
using TimLearning.Infrastructure.Interfaces.Db;
using TimLearning.Infrastructure.Interfaces.Providers.Clock;
using TimLearning.Shared.Validation.Exceptions.Localized;
using TimLearning.Shared.Validation.Validators;

namespace TimLearning.Application.UseCases.Users.Validators;

public class RefreshableTokenDtoValidator : IAsyncSimpleValidator<RefreshableTokenDto>
{
    private readonly IAppDbContext _db;
    private readonly IDateTimeProvider _dateTimeProvider;

    public RefreshableTokenDtoValidator(IAppDbContext db, IDateTimeProvider dateTimeProvider)
    {
        _db = db;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task ValidateAndThrowAsync(
        RefreshableTokenDto entity,
        CancellationToken ct = default
    )
    {
        var user = await _db.Users
            .Where(u => u.Email == entity.UserEmail)
            .Select(u => new { u.RefreshToken, u.RefreshTokenExpireAt })
            .FirstOrDefaultAsync(ct);

        if (user is null)
        {
            LocalizedValidationException.ThrowSimpleTextError(
                $"Пользователя с почтой[{entity.UserEmail}] не существует."
            );
        }
        if (user.RefreshToken != entity.RefreshToken)
        {
            LocalizedValidationException.ThrowSimpleTextError(
                "У данного пользователя другой refresh token."
            );
        }

        var now = await _dateTimeProvider.GetUtcNow();
        if (user.RefreshTokenExpireAt < now)
        {
            LocalizedValidationException.ThrowSimpleTextError("Refresh token просрочен.");
        }
    }
}
