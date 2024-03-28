namespace TimLearning.Api.Requests.Lesson;

public class UpdateLessonRequest
{
    public string? Name { get; init; }
    public string? Text { get; init; }
    public bool? IsDraft { get; init; }
}
