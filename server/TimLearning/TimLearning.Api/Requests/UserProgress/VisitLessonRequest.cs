using System.ComponentModel.DataAnnotations;

namespace TimLearning.Api.Requests.UserProgress;

public class VisitLessonRequest
{
    [Required]
    public required Guid LessonId { get; init; }
}
