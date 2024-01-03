using FluentValidation;
using TimLearning.Model.Db;
using TimLearning.Model.Dto.User;
using TimLearning.Model.Entities;
using TimLearning.Shared.Validation.FluentValidator.Extensions;

namespace TimLearning.Model.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _db;
    private readonly IValidator<NewUserDto> _newUserDtoValidator;

    public UserService(AppDbContext db, IValidator<NewUserDto> newUserDtoValidator)
    {
        _db = db;
        _newUserDtoValidator = newUserDtoValidator;
    }

    public async Task<Guid> RegisterAsync(NewUserDto dto)
    {
        await _newUserDtoValidator.ValidateAndThrowWithLocalizedErrorsAsync(dto);

        var user = new User { Email = dto.Email };

        _db.Add(user);

        await _db.SaveChangesAsync();

        return user.Id;
    }
}
