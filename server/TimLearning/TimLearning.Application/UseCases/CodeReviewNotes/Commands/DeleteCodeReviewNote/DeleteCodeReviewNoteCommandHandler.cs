using MediatR;
using Microsoft.EntityFrameworkCore;
using TimLearning.Infrastructure.Interfaces.Db;
using TimLearning.Shared.Validation.Validators;

namespace TimLearning.Application.UseCases.CodeReviewNotes.Commands.DeleteCodeReviewNote;

public class DeleteCodeReviewNoteCommandHandler : IRequestHandler<DeleteCodeReviewNoteCommand>
{
    private readonly IAppDbContext _dbContext;
    private readonly IAsyncSimpleValidator<DeleteCodeReviewNoteCommand> _commandValidator;

    public DeleteCodeReviewNoteCommandHandler(
        IAppDbContext dbContext,
        IAsyncSimpleValidator<DeleteCodeReviewNoteCommand> commandValidator
    )
    {
        _dbContext = dbContext;
        _commandValidator = commandValidator;
    }

    public async Task Handle(
        DeleteCodeReviewNoteCommand request,
        CancellationToken cancellationToken
    )
    {
        await _commandValidator.ValidateAndThrowAsync(request, cancellationToken);

        var note = await _dbContext
            .CodeReviewNotes.Where(n => n.Id == request.Id)
            .FirstAsync(cancellationToken);

        note.Delete();

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
