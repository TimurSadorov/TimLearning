namespace TimLearning.Api.Requests.Course;

public class FindCoursesRequest
{
    public Guid? Id { get; init; }
    public bool? IsDraft { get; init; }
    public bool? IsDeleted { get; init; }
}