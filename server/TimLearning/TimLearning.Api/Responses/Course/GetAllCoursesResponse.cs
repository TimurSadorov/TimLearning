using System.ComponentModel.DataAnnotations;

namespace TimLearning.Api.Responses.Course;

public record GetAllCoursesResponse(
    [property: Required] Guid Id,
    [property: Required] string Name,
    [property: Required] string Description
);
