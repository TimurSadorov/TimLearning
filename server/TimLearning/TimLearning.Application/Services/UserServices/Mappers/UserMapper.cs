using TimLearning.Application.Services.UserServices.Dto;
using TimLearning.Domain.Entities;

namespace TimLearning.Application.Services.UserServices.Mappers;

public static class UserMapper
{
    public static UserClaimsDto ToUserClaimsDto(this User user, List<UserRole> roles)
    {
        return new UserClaimsDto(user.Id, user.Email, roles.Select(r => r.Type).ToList());
    }
}
