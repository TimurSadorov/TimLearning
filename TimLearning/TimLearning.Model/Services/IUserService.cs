using TimLearning.Model.Dto.User;

namespace TimLearning.Model.Services;

public interface IUserService
{
    Task<Guid> RegisterAsync(NewUserDto dto);
}
