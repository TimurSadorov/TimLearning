using TimLearning.Application.Services.UserServices.Dto;

namespace TimLearning.Application.Services.UserServices;

public interface IUserTokenGenerator
{
    Task<AuthTokensDto> GenerateTokens(UserClaimsDto userClaims);
}
