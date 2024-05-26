using Microsoft.EntityFrameworkCore;
using TimLearning.Application.UseCases.CodeReviewNotes.Commands.CreateCodeReviewNote;
using TimLearning.Domain.Entities.Enums;
using TimLearning.Domain.Exceptions;
using TimLearning.Infrastructure.Interfaces.Db;
using TimLearning.Shared.Validation.Exceptions.Localized;
using TimLearning.Shared.Validation.Validators;

namespace TimLearning.Application.UseCases.CodeReviewNotes.Validators;

public class CreateCodeReviewNoteCommandValidator
    : IAsyncSimpleValidator<CreateCodeReviewNoteCommand>
{
    private readonly IAppDbContext _dbContext;

    public CreateCodeReviewNoteCommandValidator(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task ValidateAndThrowAsync(
        CreateCodeReviewNoteCommand entity,
        CancellationToken ct = default
    )
    {
        var dto = entity.Dto;
        var review = await _dbContext
            .CodeReviews.Where(r =>
                r.Id == dto.CodeReviewId
                && r.GroupStudent.StudyGroup.MentorId == entity.CallingUserId
            )
            .Select(r => new { r.Status })
            .FirstOrDefaultAsync(ct);
        if (review is null)
        {
            throw new NotFoundException();
        }

        if (review.Status is not CodeReviewStatus.Started)
        {
            LocalizedValidationException.ThrowSimpleTextError(
                "Ревью кода либо не было начато, либо уже закончено."
            );
        }
    }
}
