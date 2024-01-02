using TimLearning.Model.Dto.User;

namespace TimLearning.Model.Services;

public interface IUserService
{
    Task RegisterAsync(NewUserDto dto);
}
