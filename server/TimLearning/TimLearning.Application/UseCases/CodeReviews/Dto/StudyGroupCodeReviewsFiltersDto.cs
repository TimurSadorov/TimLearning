using TimLearning.Domain.Entities.Enums;

namespace TimLearning.Application.UseCases.CodeReviews.Dto;

public record StudyGroupCodeReviewsFiltersDto(Guid StudyGroupId, List<CodeReviewStatus>? Statuses);
