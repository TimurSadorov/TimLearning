using TimLearning.Api.Responses.CodeReviewNote;
using TimLearning.Domain.Entities.Data;

namespace TimLearning.Api.Mappers.CodeReviewNote;

public static class CodeReviewNotePositionDataMappers
{
    public static CodeReviewNotePositionResponse ToResponse(this CodeReviewNotePositionData data)
    {
        return new CodeReviewNotePositionResponse(data.Row, data.Column);
    }
}
