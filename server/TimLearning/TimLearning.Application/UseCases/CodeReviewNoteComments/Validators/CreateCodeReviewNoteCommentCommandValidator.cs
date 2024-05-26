using Microsoft.EntityFrameworkCore;
using TimLearning.Application.UseCases.CodeReviewNoteComments.Commands.CreateCodeReviewNoteComment;
using TimLearning.Domain.Data.ValueObjects;
using TimLearning.Domain.Exceptions;
using TimLearning.Infrastructure.Interfaces.Db;
using TimLearning.Shared.Validation.Validators;

namespace TimLearning.Application.UseCases.CodeReviewNoteComments.Validators;

public class CreateCodeReviewNoteCommentCommandValidator
    : IAsyncSimpleValidator<CreateCodeReviewNoteCommentCommand>
{
    private readonly IAppDbContext _dbContext;
    private readonly ISimpleValidator<CodeReviewNoteCommentTextValueObject> _textValidator;

    public CreateCodeReviewNoteCommentCommandValidator(
        IAppDbContext dbContext,
        ISimpleValidator<CodeReviewNoteCommentTextValueObject> textValidator
    )
    {
        _dbContext = dbContext;
        _textValidator = textValidator;
    }

    public async Task ValidateAndThrowAsync(
        CreateCodeReviewNoteCommentCommand entity,
        CancellationToken ct = default
    )
    {
        var dto = entity.Dto;
        if (
            await _dbContext.CodeReviewNotes.AnyAsync(
                n =>
                    n.Id == dto.CodeReviewNoteId
                    && n.Deleted == false
                    && (
                        n.CodeReview.GroupStudent.UserId == entity.CallingUserId
                        || n.CodeReview.GroupStudent.StudyGroup.MentorId == entity.CallingUserId
                    ),
                ct
            )
            is false
        )
        {
            throw new NotFoundException();
        }

        _textValidator.ValidateAndThrow(new CodeReviewNoteCommentTextValueObject(dto.Text));
    }
}
