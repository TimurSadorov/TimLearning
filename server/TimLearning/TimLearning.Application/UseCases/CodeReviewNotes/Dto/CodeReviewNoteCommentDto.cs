namespace TimLearning.Application.UseCases.CodeReviewNotes.Dto;

public record CodeReviewNoteCommentDto(
    Guid Id,
    string AuthorEmail,
    string Text,
    DateTimeOffset Added
);
