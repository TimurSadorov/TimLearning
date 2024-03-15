using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TimLearning.Application.UseCases.Users.Dto;
using TimLearning.Domain.Data.ValueObjects;
using TimLearning.Infrastructure.Interfaces.Db;

namespace TimLearning.Application.UseCases.Users.Validators;

public class NewUserDtoValidator : AbstractValidator<NewUserDto>
{
    public NewUserDtoValidator(
        IAppDbContext db,
        IValidator<UserPasswordValueObject> userPasswordValidator
    )
    {
        RuleFor(dto => dto.Email)
            .NotEmpty()
            .OverridePropertyName("email")
            .WithMessage("Email не может быть пустым.")
            .EmailAddress()
            .WithMessage("Email имеет невалидный формат.")
            .MustAsync(
                async (email, ct) => await db.Users.AnyAsync(u => u.Email == email, ct) == false
            )
            .WithMessage("Пользователь с такой почтой уже существует.");

        RuleFor(dto => new UserPasswordValueObject(dto.Password))
            .SetValidator(userPasswordValidator)
            .OverridePropertyName("password");
    }
}
