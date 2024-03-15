using MediatR;
using TimLearning.Application.Mediator.Pipelines.RoleAccess;
using TimLearning.Application.UseCases.Courses.Dto;
using TimLearning.Domain.Entities.Enums;

namespace TimLearning.Application.UseCases.Courses.Queries.FindCourse;

public record FindCourseQuery(
    Guid CallingUserId,
    string? SearchName = null,
    bool? IsDraft = null, 
    bool? IsDeleted = null
) : IRequest<List<CourseDto>>, IAccessByRole
{
    public static IEnumerable<UserRoleType> ForRoles => [UserRoleType.ContentCreator];
}