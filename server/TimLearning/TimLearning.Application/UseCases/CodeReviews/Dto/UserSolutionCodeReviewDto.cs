using TimLearning.Domain.Entities.Enums;

namespace TimLearning.Application.UseCases.CodeReviews.Dto;

public record UserSolutionCodeReviewDto(
    CodeReviewStatus Status,
    string UserEmail,
    CodeReviewLessonDto Lesson,
    UserSolutionDto Solution,
    string StandardCode
);
