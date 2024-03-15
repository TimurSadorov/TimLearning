using Microsoft.EntityFrameworkCore;
using TimLearning.Application.UseCases.Users.Dto;
using TimLearning.Infrastructure.Interfaces.Db;
using TimLearning.Shared.Validation.Exceptions.Localized;
using TimLearning.Shared.Validation.Validators;

namespace TimLearning.Application.UseCases.Users.Validators;

public class NewUserEmailConfirmationDtoValidator
    : IAsyncSimpleValidator<NewUserEmailConfirmationDto>
{
    private readonly IAppDbContext _dbContext;

    public NewUserEmailConfirmationDtoValidator(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task ValidateAndThrowAsync(
        NewUserEmailConfirmationDto entity,
        CancellationToken ct = default
    )
    {
        var user = await _dbContext.Users
            .Where(u => u.Email == entity.UserEmail)
            .Select(u => new { u.IsEmailConfirmed })
            .SingleOrDefaultAsync(ct);

        if (user is null)
        {
            LocalizedValidationException.ThrowWithSimpleTextError(
                "Пользователь с такой почтой не найден."
            );
        }

        if (user.IsEmailConfirmed)
        {
            LocalizedValidationException.ThrowWithSimpleTextError("Почта уже подтверждена.");
        }
    }
}
