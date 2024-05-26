using System.ComponentModel.DataAnnotations;

namespace TimLearning.Api.Responses.CodeReviewNote;

public record CodeReviewNoteCommentResponse(
    [property: Required] Guid Id,
    [property: Required] string AuthorEmail,
    [property: Required] string Text,
    [property: Required] DateTimeOffset Added
);
