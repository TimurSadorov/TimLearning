using System.ComponentModel.DataAnnotations;

namespace TimLearning.Api.Responses.Course;

public record GetUserCoursesResponse(
    [property: Required] Guid Id,
    [property: Required] string Name,
    [property: Required] string ShortName,
    [property: Required] string Description
);
