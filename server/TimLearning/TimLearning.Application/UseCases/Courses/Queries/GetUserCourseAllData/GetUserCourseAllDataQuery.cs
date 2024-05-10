using MediatR;
using TimLearning.Application.Mediator.Pipelines.RoleAccess;
using TimLearning.Application.UseCases.Courses.Dto;
using TimLearning.Domain.Access;
using TimLearning.Domain.Entities.Enums;

namespace TimLearning.Application.UseCases.Courses.Queries.GetUserCourseAllData;

public record GetUserCourseAllDataQuery(Guid CourseId, Guid CallingUserId)
    : IRequest<UserCourseAllDataDto>,
        IAccessByRole
{
    public static IEnumerable<UserRoleType> ForRoles => AccessGroup.Everyone;
}
