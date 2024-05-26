using MediatR;
using TimLearning.Domain.Entities;
using TimLearning.Infrastructure.Interfaces.Db;
using TimLearning.Infrastructure.Interfaces.Providers.Clock;
using TimLearning.Shared.Validation.Validators;

namespace TimLearning.Application.UseCases.CodeReviewNoteComments.Commands.CreateCodeReviewNoteComment;

public class CreateCodeReviewNoteCommentCommandHandler
    : IRequestHandler<CreateCodeReviewNoteCommentCommand>
{
    private readonly IAppDbContext _dbContext;
    private readonly IAsyncSimpleValidator<CreateCodeReviewNoteCommentCommand> _commandValidator;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CreateCodeReviewNoteCommentCommandHandler(
        IAppDbContext dbContext,
        IAsyncSimpleValidator<CreateCodeReviewNoteCommentCommand> commandValidator,
        IDateTimeProvider dateTimeProvider
    )
    {
        _dbContext = dbContext;
        _commandValidator = commandValidator;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task Handle(
        CreateCodeReviewNoteCommentCommand request,
        CancellationToken cancellationToken
    )
    {
        await _commandValidator.ValidateAndThrowAsync(request, cancellationToken);

        var dto = request.Dto;

        var now = await _dateTimeProvider.GetUtcNow();
        var comment = new CodeReviewNoteComment
        {
            CodeReviewNoteId = dto.CodeReviewNoteId,
            Text = dto.Text,
            AuthorId = request.CallingUserId,
            Added = now
        };
        _dbContext.Add(comment);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
