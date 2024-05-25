using System.ComponentModel.DataAnnotations;

namespace TimLearning.Api.Requests.StudyGroup;

public class CreateStudyGroupRequest
{
    [Required]
    public required Guid CourseId { get; init; }

    [Required]
    public required string Name { get; init; }
}
