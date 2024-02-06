using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TimLearning.Application.Specifications.Dynamic.Users;
using TimLearning.Application.UseCases.Users.Dto;
using TimLearning.Infrastructure.Interfaces.Db;
using TimLearning.Shared.Validation.FluentValidator.Validators;

namespace TimLearning.Application.Validators.Users;

public class NewUserDtoValidator : TimLearningFluentValidator<NewUserDto>
{
    public NewUserDtoValidator(IAppDbContext db)
    {
        RuleFor(dto => dto.Email)
            .NotEmpty()
            .OverridePropertyName("email")
            .WithMessage("Email не может быть пустым.")
            .EmailAddress()
            .WithMessage("Email имеет невалидный формат.")
            .MustAsync(
                async (email, ct) =>
                    await db.Users.AnyAsync(new UserByEmailSpecification(email), ct) == false
            )
            .WithMessage("Пользователь с такой почтой уже существует.");

        RuleFor(dto => dto.Password)
            .MinimumLength(8)
            .OverridePropertyName("password")
            .WithMessage("Пароль должен содержать не меньше 8 символов.");
    }
}
