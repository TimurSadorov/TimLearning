using TimLearning.Domain.Entities.Enums;

namespace TimLearning.Application.UseCases.CodeReviews.Dto;

public record StudyGroupCodeReviewDto(
    Guid Id,
    CodeReviewStatus Status,
    DateTimeOffset? Completed,
    string UserEmail,
    string ModuleName,
    string LessonName
);
