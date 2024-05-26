using System.ComponentModel.DataAnnotations;

namespace TimLearning.Api.Responses.CodeReviewNote;

public record CodeReviewNoteWithCommentResponse(
    [property: Required] Guid Id,
    [property: Required] CodeReviewNotePositionResponse StartPosition,
    [property: Required] CodeReviewNotePositionResponse EndPosition,
    [property: Required] List<CodeReviewNoteCommentResponse> Comments
);
