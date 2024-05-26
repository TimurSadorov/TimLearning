using MediatR;
using Microsoft.EntityFrameworkCore;
using TimLearning.Infrastructure.Interfaces.Db;
using TimLearning.Shared.Validation.Validators;

namespace TimLearning.Application.UseCases.CodeReviews.Commands.StartCodeReview;

public class StartCodeReviewCommandHandler : IRequestHandler<StartCodeReviewCommand>
{
    private readonly IAppDbContext _dbContext;
    private readonly IAsyncSimpleValidator<StartCodeReviewCommand> _commandValidator;

    public StartCodeReviewCommandHandler(
        IAppDbContext dbContext,
        IAsyncSimpleValidator<StartCodeReviewCommand> commandValidator
    )
    {
        _dbContext = dbContext;
        _commandValidator = commandValidator;
    }

    public async Task Handle(StartCodeReviewCommand request, CancellationToken cancellationToken)
    {
        await _commandValidator.ValidateAndThrowAsync(request, cancellationToken);

        var review = await _dbContext.CodeReviews.FirstAsync(
            r => r.Id == request.CodeReviewId,
            cancellationToken
        );

        review.Start();

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
