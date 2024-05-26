using TimLearning.Domain.Data.ValueObjects;
using TimLearning.Shared.Extensions;
using TimLearning.Shared.Validation.Exceptions.Localized;
using TimLearning.Shared.Validation.Validators;

namespace TimLearning.Domain.Validators;

public class CodeReviewNoteCommentTextValidator
    : ISimpleValidator<CodeReviewNoteCommentTextValueObject>
{
    public void ValidateAndThrow(CodeReviewNoteCommentTextValueObject entity)
    {
        if (entity.Value.HasText() is false)
        {
            LocalizedValidationException.ThrowSimpleTextError(
                "Текст комментария не может быть пустым."
            );
        }
    }
}
