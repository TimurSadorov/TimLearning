using LinqSpecs.Core;
using TimLearning.Domain.Entities;

namespace TimLearning.Application.Specifications;

public static class CodeReviewNoteCommentSpecifications
{
    public static Specification<CodeReviewNoteComment> AvailableToUser(Guid userId) =>
        new AdHocSpecification<CodeReviewNoteComment>(c =>
            c.Deleted == false && c.AuthorId == userId
        );
}
