using MediatR;
using TimLearning.Application.UseCases.CodeReviewNoteComments.Commands.CreateCodeReviewNoteComment;
using TimLearning.Application.UseCases.CodeReviewNoteComments.Dto;
using TimLearning.Domain.Entities;
using TimLearning.Infrastructure.Interfaces.Db;
using TimLearning.Infrastructure.Interfaces.Providers.Clock;
using TimLearning.Shared.Validation.Validators;

namespace TimLearning.Application.UseCases.CodeReviewNotes.Commands.CreateCodeReviewNote;

public class CreateCodeReviewNoteCommandHandler : IRequestHandler<CreateCodeReviewNoteCommand, Guid>
{
    private readonly IAppDbContext _dbContext;
    private readonly IAsyncSimpleValidator<CreateCodeReviewNoteCommand> _commandValidator;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IMediator _mediator;

    public CreateCodeReviewNoteCommandHandler(
        IAppDbContext dbContext,
        IAsyncSimpleValidator<CreateCodeReviewNoteCommand> commandValidator,
        IDateTimeProvider dateTimeProvider,
        IMediator mediator
    )
    {
        _dbContext = dbContext;
        _commandValidator = commandValidator;
        _dateTimeProvider = dateTimeProvider;
        _mediator = mediator;
    }

    public async Task<Guid> Handle(
        CreateCodeReviewNoteCommand request,
        CancellationToken cancellationToken
    )
    {
        await _commandValidator.ValidateAndThrowAsync(request, cancellationToken);

        var dto = request.Dto;
        var now = await _dateTimeProvider.GetUtcNow();
        var note = new CodeReviewNote
        {
            CodeReviewId = dto.CodeReviewId,
            StartPosition = dto.StartPosition,
            EndPosition = dto.EndPosition,
            Added = now
        };
        _dbContext.Add(note);

        await _dbContext.SaveChangesAsync(cancellationToken);

        await _mediator.Send(
            new CreateCodeReviewNoteCommentCommand(
                new NewCodeReviewNoteCommentDto(note.Id, dto.InitCommentText),
                request.CallingUserId
            ),
            cancellationToken
        );

        return note.Id;
    }
}
