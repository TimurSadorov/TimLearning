using System.ComponentModel.DataAnnotations;

namespace TimLearning.Api.Requests.CodeReviewNote;

public class CreateCodeReviewNoteRequest
{
    [Required]
    public required CodeReviewNotePositionRequest StartPosition { get; init; }

    [Required]
    public required CodeReviewNotePositionRequest EndPosition { get; init; }

    [Required]
    public required string InitCommentText { get; init; }
}
