namespace TimLearning.Api.Requests.Course;

public record UpdateCourseRequest
{
    public string? Name { get; init; }
    public string? ShortName { get; init; }
    public string? Description { get; init; }
    public bool? IsDraft { get; init; }
    public bool? IsDeleted { get; init; }
}
