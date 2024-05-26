using System.ComponentModel.DataAnnotations;

namespace TimLearning.Api.Responses.CodeReviewNote;

public record CodeReviewNotePositionResponse(
    [property: Required] int Row,
    [property: Required] int Column
);
