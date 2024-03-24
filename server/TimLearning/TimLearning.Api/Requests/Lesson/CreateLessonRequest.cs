using System.ComponentModel.DataAnnotations;

namespace TimLearning.Api.Requests.Lesson;

public class CreateLessonRequest
{
    [Required]
    public required string Name { get; init; }
}
