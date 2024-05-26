using System.ComponentModel.DataAnnotations;

namespace TimLearning.Api.Requests.CodeReviewNoteComment;

public class CreateCodeReviewNoteCommentRequest
{
    [Required]
    public required string Text { get; init; }
}
