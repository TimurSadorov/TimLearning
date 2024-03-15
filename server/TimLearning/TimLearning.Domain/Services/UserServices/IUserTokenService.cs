using TimLearning.Domain.Entities;
using TimLearning.Domain.Entities.Enums;
using TimLearning.Domain.Services.UserServices.Dto;

namespace TimLearning.Domain.Services.UserServices;

public interface IUserTokenService
{
    AuthTokensDto UpdateTokens(User user, List<UserRoleType> userRoles, DateTimeOffset now);
}
