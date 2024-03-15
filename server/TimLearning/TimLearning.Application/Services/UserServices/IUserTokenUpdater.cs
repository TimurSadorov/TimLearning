using TimLearning.Domain.Services.UserServices.Dto;

namespace TimLearning.Application.Services.UserServices;

public interface IUserTokenUpdater
{
    Task<AuthTokensDto> UpdateUserTokens(string email);
}
