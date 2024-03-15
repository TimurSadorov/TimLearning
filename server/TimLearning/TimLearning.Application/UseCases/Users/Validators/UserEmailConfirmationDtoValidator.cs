using Microsoft.EntityFrameworkCore;
using TimLearning.Application.Services.UserServices;
using TimLearning.Application.UseCases.Users.Dto;
using TimLearning.Infrastructure.Interfaces.Db;
using TimLearning.Shared.Validation.Exceptions.Localized;
using TimLearning.Shared.Validation.Validators;

namespace TimLearning.Application.UseCases.Users.Validators;

public class UserEmailConfirmationDtoValidator : IAsyncSimpleValidator<UserEmailConfirmationDto>
{
    private readonly IAppDbContext _db;
    private readonly IUserDataEncryptor _userUserDataEncryptor;

    public UserEmailConfirmationDtoValidator(IAppDbContext db, IUserDataEncryptor userDataEncryptor)
    {
        _db = db;
        _userUserDataEncryptor = userDataEncryptor;
    }

    public async Task ValidateAndThrowAsync(
        UserEmailConfirmationDto entity,
        CancellationToken ct = default
    )
    {
        var user = await _db.Users
            .Where(u => u.Email == entity.Email)
            .Select(u => new { u.IsEmailConfirmed })
            .FirstOrDefaultAsync(ct);
        if (user is null)
        {
            LocalizedValidationException.ThrowWithSimpleTextError(
                $"Пользователь с почтой[{entity.Email}] не найден."
            );
        }

        if (user.IsEmailConfirmed)
        {
            LocalizedValidationException.ThrowWithSimpleTextError("Почта уже подтверждена.");
        }

        if (_userUserDataEncryptor.VerifyEmail(entity.Signature, entity.Email) == false)
        {
            LocalizedValidationException.ThrowWithSimpleTextError(
                "Подтверждение не предназначено для этой почты."
            );
        }
    }
}
