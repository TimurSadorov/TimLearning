using Microsoft.EntityFrameworkCore;
using TimLearning.Application.Data.ValueObjects;
using TimLearning.Application.Specifications.Dynamic.Users;
using TimLearning.Infrastructure.Interfaces.Db;
using TimLearning.Shared.Validation.Exceptions.Localized;
using TimLearning.Shared.Validation.Validators;

namespace TimLearning.Application.Validators.Users;

public class UserEmailValidator : IAsyncSimpleValidator<UserEmailValueObject>
{
    private readonly IAppDbContext _db;

    public UserEmailValidator(IAppDbContext db)
    {
        _db = db;
    }

    public async Task ValidateAndThrowAsync(
        UserEmailValueObject entity,
        CancellationToken ct = default
    )
    {
        if (await _db.Users.AnyAsync(new UserByEmailSpecification(entity.Value), ct) == false)
        {
            LocalizedValidationException.ThrowWithSimpleTextError(
                "Пользователь с такой почтой не найден."
            );
        }
    }
}
