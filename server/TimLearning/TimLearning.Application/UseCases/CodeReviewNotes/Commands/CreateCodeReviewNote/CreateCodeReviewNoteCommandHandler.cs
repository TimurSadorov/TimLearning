using MediatR;
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

    public CreateCodeReviewNoteCommandHandler(
        IAppDbContext dbContext,
        IAsyncSimpleValidator<CreateCodeReviewNoteCommand> commandValidator,
        IDateTimeProvider dateTimeProvider
    )
    {
        _dbContext = dbContext;
        _commandValidator = commandValidator;
        _dateTimeProvider = dateTimeProvider;
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

        return note.Id;
    }
}
