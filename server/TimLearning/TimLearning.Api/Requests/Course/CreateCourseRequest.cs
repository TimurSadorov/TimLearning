using System.ComponentModel.DataAnnotations;

namespace TimLearning.Api.Requests.Course;

public class CreateCourseRequest
{
    [Required]
    public required string Name { get; init; }

    [Required]
    public required string ShortName { get; init; }

    [Required]
    public required string Description { get; init; }
}
