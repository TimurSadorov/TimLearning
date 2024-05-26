using TimLearning.Domain.Entities.Data;

namespace TimLearning.Application.UseCases.CodeReviewNotes.Dto;

public record NewCodeReviewNote(
    Guid CodeReviewId,
    CodeReviewNotePositionData StartPosition,
    CodeReviewNotePositionData EndPosition
);
