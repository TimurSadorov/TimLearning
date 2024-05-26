using TimLearning.Api.Requests.CodeReviewNote;
using TimLearning.Domain.Entities.Data;

namespace TimLearning.Api.Mappers.CodeReviewNote;

public static class CodeReviewNotePositionRequestMappers
{
    public static CodeReviewNotePositionData ToData(this CodeReviewNotePositionRequest request)
    {
        return new CodeReviewNotePositionData { Row = request.Row, Column = request.Column };
    }
}
