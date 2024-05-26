using TimLearning.Domain.Entities.Enums;

namespace TimLearning.Application.Services.CodeReviewServices;

public interface ICodeReviewService
{
    Task AddReviewsForUserSolution(Guid solutionId, CancellationToken ct = default);

    Task<CodeReviewStatus> GetStatusAvailableToGroupMentor(
        Guid id,
        Guid mentorId,
        CancellationToken ct = default
    );
}
