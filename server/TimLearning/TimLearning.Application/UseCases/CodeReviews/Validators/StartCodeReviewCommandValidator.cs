using TimLearning.Application.Services.CodeReviewServices;
using TimLearning.Application.UseCases.CodeReviews.Commands.StartCodeReview;
using TimLearning.Domain.Entities.Enums;
using TimLearning.Shared.Validation.Exceptions.Localized;
using TimLearning.Shared.Validation.Validators;

namespace TimLearning.Application.UseCases.CodeReviews.Validators;

public class StartCodeReviewCommandValidator : IAsyncSimpleValidator<StartCodeReviewCommand>
{
    private readonly ICodeReviewService _codeReviewService;

    public StartCodeReviewCommandValidator(ICodeReviewService codeReviewService)
    {
        _codeReviewService = codeReviewService;
    }

    public async Task ValidateAndThrowAsync(
        StartCodeReviewCommand entity,
        CancellationToken ct = default
    )
    {
        var status = await _codeReviewService.GetStatusAvailableToGroupMentor(
            entity.CodeReviewId,
            entity.CallingUserId,
            ct
        );

        if (status is not CodeReviewStatus.Pending)
        {
            LocalizedValidationException.ThrowSimpleTextError("Ревью кода уже было начато.");
        }
    }
}
