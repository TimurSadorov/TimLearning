using System.ComponentModel.DataAnnotations;

namespace TimLearning.Api.Requests.CodeReviewNote;

public class CodeReviewNotePositionRequest
{
    [Required]
    public required int Row { get; init; }

    [Required]
    public required int Column { get; init; }
}
