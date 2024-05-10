namespace TimLearning.Application.UseCases.Courses.Dto;

public record UserCourseAllDataDto(
    string ShortName,
    int CompletionPercentage,
    List<UserProgressInModuleDto> Modules
);
