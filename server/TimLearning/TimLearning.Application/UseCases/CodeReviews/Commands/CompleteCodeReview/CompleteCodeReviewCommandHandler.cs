using MediatR;
using Microsoft.EntityFrameworkCore;
using TimLearning.Infrastructure.Interfaces.Db;
using TimLearning.Infrastructure.Interfaces.Providers.Clock;
using TimLearning.Shared.Validation.Validators;

namespace TimLearning.Application.UseCases.CodeReviews.Commands.CompleteCodeReview;

public class CompleteCodeReviewCommandHandler : IRequestHandler<CompleteCodeReviewCommand>
{
    private readonly IAppDbContext _dbContext;
    private readonly IAsyncSimpleValidator<CompleteCodeReviewCommand> _commandValidator;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CompleteCodeReviewCommandHandler(
        IAppDbContext dbContext,
        IAsyncSimpleValidator<CompleteCodeReviewCommand> commandValidator,
        IDateTimeProvider dateTimeProvider
    )
    {
        _dbContext = dbContext;
        _commandValidator = commandValidator;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task Handle(CompleteCodeReviewCommand request, CancellationToken cancellationToken)
    {
        await _commandValidator.ValidateAndThrowAsync(request, cancellationToken);

        var review = await _dbContext.CodeReviews.FirstAsync(
            r => r.Id == request.CodeReviewId,
            cancellationToken
        );

        var now = await _dateTimeProvider.GetUtcNow();
        if (request.IsSuccess)
        {
            review.Complete(now);
        }
        else
        {
            review.Reject(now);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
