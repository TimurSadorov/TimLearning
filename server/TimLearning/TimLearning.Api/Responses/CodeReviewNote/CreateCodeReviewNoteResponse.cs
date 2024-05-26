using System.ComponentModel.DataAnnotations;

namespace TimLearning.Api.Responses.CodeReviewNote;

public record CreateCodeReviewNoteResponse([property: Required] Guid Id);
