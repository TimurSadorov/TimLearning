using FluentValidation;
using TimLearning.Domain.Data.ValueObjects;

namespace TimLearning.Domain.Validators;

public class UserPasswordValidator : AbstractValidator<UserPasswordValueObject>
{
    public UserPasswordValidator()
    {
        RuleFor(userPassword => userPassword.Value)
            .MinimumLength(8)
            .WithMessage("Пароль должен содержать не меньше 8 символов.")
            .OverridePropertyName(string.Empty);
    }
}
