using System.ComponentModel.DataAnnotations;

namespace TimLearning.Api.Requests.CodeReviewNoteComment;

public class UpdateCodeReviewNoteCommentRequest
{
    [Required]
    public required string Text { get; init; }
}
