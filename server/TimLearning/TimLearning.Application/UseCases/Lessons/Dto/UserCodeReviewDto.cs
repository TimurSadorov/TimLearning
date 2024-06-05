using TimLearning.Domain.Entities.Enums;

namespace TimLearning.Application.UseCases.Lessons.Dto;

public record UserCodeReviewDto(Guid Id, CodeReviewStatus Status);
