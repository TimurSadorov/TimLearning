namespace TimLearning.Application.Services.CodeReviewServices;

public interface ICodeReviewService
{
    Task AddReviewsForUserSolution(Guid solutionId, CancellationToken ct = default);
}
