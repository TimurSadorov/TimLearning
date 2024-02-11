using TimLearning.Domain.Entities.Enums;

namespace TimLearning.Application.Services.UserServices.Dto;

public record UserClaimsDto(Guid Id, string Email, List<UserRoleType> Roles);
