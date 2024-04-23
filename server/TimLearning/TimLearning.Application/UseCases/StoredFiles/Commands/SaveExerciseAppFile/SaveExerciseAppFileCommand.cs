using MediatR;
using TimLearning.Application.Mediator.Pipelines.RoleAccess;
using TimLearning.Application.UseCases.StoredFiles.Dto;
using TimLearning.Domain.Entities.Enums;

namespace TimLearning.Application.UseCases.StoredFiles.Commands.SaveExerciseAppFile;

public record SaveExerciseAppFileCommand(FileDto FileDto, Guid CallingUserId)
    : IRequest<Guid>,
        IAccessByRole
{
    public static IEnumerable<UserRoleType> ForRoles { get; } = [UserRoleType.ContentCreator];
}
