using Microsoft.EntityFrameworkCore;
using TimLearning.Application.UseCases.CodeReviewNotes.Commands.DeleteCodeReviewNote;
using TimLearning.Domain.Exceptions;
using TimLearning.Infrastructure.Interfaces.Db;
using TimLearning.Shared.Validation.Validators;

namespace TimLearning.Application.UseCases.CodeReviewNotes.Validators;

public class DeleteCodeReviewNoteCommandValidator
    : IAsyncSimpleValidator<DeleteCodeReviewNoteCommand>
{
    private readonly IAppDbContext _dbContext;

    public DeleteCodeReviewNoteCommandValidator(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task ValidateAndThrowAsync(
        DeleteCodeReviewNoteCommand entity,
        CancellationToken ct = default
    )
    {
        if (
            await _dbContext
                .CodeReviewNotes.Where(n =>
                    n.Id == entity.Id
                    && n.CodeReview.GroupStudent.StudyGroup.MentorId == entity.CallingUserId
                )
                .AnyAsync(ct)
            is false
        )
        {
            throw new NotFoundException();
        }
    }
}
