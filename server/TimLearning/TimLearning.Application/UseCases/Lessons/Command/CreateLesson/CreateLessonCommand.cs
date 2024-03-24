using MediatR;
using TimLearning.Application.Mediator.Pipelines.RoleAccess;
using TimLearning.Application.UseCases.Lessons.Dto;
using TimLearning.Domain.Entities.Enums;

namespace TimLearning.Application.UseCases.Lessons.Command.CreateLesson;

public record CreateLessonCommand(NewLessonDto Dto, Guid CallingUserId) : IRequest, IAccessByRole
{
    public static IEnumerable<UserRoleType> ForRoles { get; } = [UserRoleType.ContentCreator];
} 
