using System.ComponentModel.DataAnnotations;

namespace TimLearning.Api.Responses.Course;

public record UserCourseAllDataResponse(
    [property: Required] string ShortName,
    [property: Required] int CompletionPercentage,
    [property: Required] List<UserProgressInModuleResponse> Modules
);
