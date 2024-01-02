using TimLearning.Model.Db;
using TimLearning.Model.Dto.User;
using TimLearning.Model.Entities;

namespace TimLearning.Model.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _db;

    public UserService(AppDbContext db)
    {
        _db = db;
    }

    public async Task RegisterAsync(NewUserDto dto)
    {
        var user = new User { Email = dto.Email };

        _db.Add(user);

        var registrationResult = await _userManager.CreateAsync(user, password);
        if (!registrationResult.Succeeded)
        {
            throw new ValidationException(
                "Ошибка в одном из полей пользователя.",
                registrationResult.Errors.Select(
                    error => new ValidationFailure("UserFields", error.Description)
                )
            );
        }
    }
}
