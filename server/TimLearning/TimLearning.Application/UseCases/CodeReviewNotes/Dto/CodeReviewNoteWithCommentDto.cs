using TimLearning.Domain.Entities.Data;

namespace TimLearning.Application.UseCases.CodeReviewNotes.Dto;

public record CodeReviewNoteWithCommentDto(
    Guid Id,
    CodeReviewNotePositionData StartPosition,
    CodeReviewNotePositionData EndPosition,
    List<CodeReviewNoteCommentDto> Comments
);
