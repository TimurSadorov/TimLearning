using MediatR;
using TimLearning.Application.Mediator.Pipelines.RoleAccess;
using TimLearning.Application.UseCases.Courses.Dto;
using TimLearning.Domain.Entities.Enums;

namespace TimLearning.Application.UseCases.Courses.Commands.CreateCourse;

public record CreateCourseCommand(NewCourseDto Dto, Guid CallingUserId) : IRequest, IAccessByRole
{
    public static IEnumerable<UserRoleType> ForRoles { get; } = [UserRoleType.ContentCreator];
}
