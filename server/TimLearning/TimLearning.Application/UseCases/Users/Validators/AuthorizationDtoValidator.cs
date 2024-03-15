using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using TimLearning.Application.UseCases.Users.Dto;
using TimLearning.Domain.Services.UserServices;
using TimLearning.Infrastructure.Interfaces.Db;
using TimLearning.Shared.Validation.Exceptions.Localized;
using TimLearning.Shared.Validation.Validators;

namespace TimLearning.Application.UseCases.Users.Validators;

public class AuthorizationDtoValidator : IAsyncSimpleValidator<AuthorizationDto>
{
    private readonly IAppDbContext _db;
    private readonly IUserPasswordService _userPasswordService;

    public AuthorizationDtoValidator(IAppDbContext db, IUserPasswordService userPasswordService)
    {
        _db = db;
        _userPasswordService = userPasswordService;
    }

    public async Task ValidateAndThrowAsync(AuthorizationDto entity, CancellationToken ct = default)
    {
        var user = await _db.Users
            .Where(u => u.Email == entity.Email)
            .Select(u => new { u.PasswordHash, u.PasswordSalt })
            .FirstOrDefaultAsync(ct);

        if (user is null)
        {
            ThrowInvalidEmailOrPassword();
        }

        if (
            _userPasswordService.IsValidPassword(
                entity.Password,
                user.PasswordHash,
                user.PasswordSalt
            ) == false
        )
        {
            ThrowInvalidEmailOrPassword();
        }
    }

    [DoesNotReturn]
    private static void ThrowInvalidEmailOrPassword()
    {
        LocalizedValidationException.ThrowWithSimpleTextError(
            "Неверный пароль или пользователь с такой почтой не существует."
        );
    }
}
