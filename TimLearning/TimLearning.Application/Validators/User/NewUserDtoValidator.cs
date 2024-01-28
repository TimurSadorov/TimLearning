using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TimLearning.Application.Specifications.Dynamic.Users;
using TimLearning.Application.UseCases.Users.Dto;
using TimLearning.Infrastructure.Interfaces.Db;

namespace TimLearning.Application.Validators.User;

public class NewUserDtoValidator : AbstractValidator<NewUserDto>
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
