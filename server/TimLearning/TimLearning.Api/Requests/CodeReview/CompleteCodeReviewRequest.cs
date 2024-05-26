using System.ComponentModel.DataAnnotations;

namespace TimLearning.Api.Requests.CodeReview;

public class CompleteCodeReviewRequest
{
    [Required]
    public required bool IsSuccess { get; init; }
}
