using MediatR;
using TimLearning.Application.Mediator.Pipelines.RoleAccess;
using TimLearning.Application.UseCases.Lessons.Dto;
using TimLearning.Domain.Entities.Enums;

namespace TimLearning.Application.UseCases.Lessons.Queries.GetLessonWithExercise;

public record GetLessonWithExerciseQuery(Guid LessonId, Guid CallingUserId)
    : IRequest<LessonWithExerciseDto>,
        IAccessByRole
{
    public static IEnumerable<UserRoleType> ForRoles { get; } = [UserRoleType.ContentCreator];
}
