using TimLearning.Api.Responses.User;
using TimLearning.Domain.Services.UserServices.Dto;

namespace TimLearning.Api.Mappers.User;

public static class AuthTokensDtoMappers
{
    public static AuthTokensResponse ToResponse(this AuthTokensDto dto)
    {
        return new AuthTokensResponse(dto.AccessToken, dto.RefreshToken);
    }
}
