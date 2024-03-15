using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TimLearning.Application.Services.UserServices;
using TimLearning.Application.UseCases.Users.Dto;
using TimLearning.Domain.Data.ValueObjects;
using TimLearning.Infrastructure.Interfaces.Db;
using TimLearning.Shared.Validation.Exceptions.Localized;
using TimLearning.Shared.Validation.FluentValidator.Validators;

namespace TimLearning.Application.UseCases.Users.Validators;

public class NewRecoveringPasswordDtoValidator
    : CombinedFluentAndSimpleValidator<NewRecoveringPasswordDto>
{
    private readonly IAppDbContext _db;
    private readonly IValidator<UserPasswordValueObject> _userPasswordValidator;
    private readonly IUserDataEncryptor _userDataEncryptor;

    public NewRecoveringPasswordDtoValidator(
        IAppDbContext db,
        IValidator<UserPasswordValueObject> userPasswordValidator,
        IUserDataEncryptor userDataEncryptor
    )
    {
        _db = db;
        _userPasswordValidator = userPasswordValidator;
        _userDataEncryptor = userDataEncryptor;
    }

    protected override Task ConfigureFluentRulesAsync(
        NewRecoveringPasswordDto entity,
        CancellationToken ct
    )
    {
        RuleFor(dto => new UserPasswordValueObject(dto.NewPassword))
            .SetValidator(_userPasswordValidator)
            .OverridePropertyName("newPassword");

        return Task.CompletedTask;
    }

    protected override async Task SimpleValidateAndThrowAsync(
        NewRecoveringPasswordDto entity,
        CancellationToken ct
    )
    {
        var user = await _db.Users
            .Where(u => u.Email == entity.UserEmail)
            .Select(u => new { u.PasswordHash })
            .FirstOrDefaultAsync(ct);
        if (user is null)
        {
            LocalizedValidationException.ThrowWithSimpleTextError(
                $"Пользователь с почтой[{entity.UserEmail}] не найден."
            );
        }

        if (
            _userDataEncryptor.VerifyEmailAndPassword(
                entity.Signature,
                entity.UserEmail,
                user.PasswordHash
            ) == false
        )
        {
            LocalizedValidationException.ThrowWithSimpleTextError(
                "Невалидная ссылка для смены пароля."
            );
        }
    }
}
