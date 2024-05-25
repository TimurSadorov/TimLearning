using System.ComponentModel.DataAnnotations;

namespace TimLearning.Api.Responses.StudyGroup;

public record StudyGroupResponse(
    [property: Required] Guid Id,
    [property: Required] string Name,
    [property: Required] bool IsActive,
    [property: Required] Guid CourseId
);
