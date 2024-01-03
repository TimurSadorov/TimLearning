using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TimLearning.Model.Db;
using TimLearning.Model.Dto.User;

namespace TimLearning.Model.Validators;

public class NewUserDtoValidator : AbstractValidator<NewUserDto>
{
    public NewUserDtoValidator(AppDbContext db)
    {
        RuleFor(dto => dto.Email)
            .NotEmpty()
            .OverridePropertyName("email")
            .WithMessage("Email не может быть пустым.")
            .EmailAddress()
            .WithMessage("Email имеет не валидный формат.")
            .MustAsync(
                async (email, ct) => await db.Users.AnyAsync(u => u.Email == email, ct) == false
            )
            .WithMessage("Пользователь с такой почтой уже существует.");

        RuleFor(dto => dto.Password)
            .MinimumLength(8)
            .OverridePropertyName("password")
            .WithMessage("Пароль должен содержать не меньше 8 символов.");
    }
}
