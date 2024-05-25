using TimLearning.Domain.Entities.Enums;

namespace TimLearning.Api.Requests.CodeReview;

public class GetStudyGroupCodeReviewsRequest
{
    public List<CodeReviewStatus>? Statuses { get; init; }
}
