using TimLearning.Application.Services.CodeReviewServices;
using TimLearning.Application.UseCases.CodeReviews.Commands.CompleteCodeReview;
using TimLearning.Domain.Entities.Enums;
using TimLearning.Shared.Validation.Exceptions.Localized;
using TimLearning.Shared.Validation.Validators;

namespace TimLearning.Application.UseCases.CodeReviews.Validators;

public class CompleteCodeReviewCommandValidator : IAsyncSimpleValidator<CompleteCodeReviewCommand>
{
    private readonly ICodeReviewService _codeReviewService;

    public CompleteCodeReviewCommandValidator(ICodeReviewService codeReviewService)
    {
        _codeReviewService = codeReviewService;
    }

    public async Task ValidateAndThrowAsync(
        CompleteCodeReviewCommand entity,
        CancellationToken ct = default
    )
    {
        var status = await _codeReviewService.GetStatusAvailableToGroupMentor(
            entity.CodeReviewId,
            entity.CallingUserId,
            ct
        );

        if (status is not CodeReviewStatus.Started)
        {
            LocalizedValidationException.ThrowSimpleTextError(
                "Ревью кода либо не было начато, либо уже закончено."
            );
        }
    }
}
