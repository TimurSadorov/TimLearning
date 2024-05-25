namespace TimLearning.Api.Requests.StudyGroup;

public class FindStudyGroupsRequest
{
    public List<Guid>? Ids { get; init; }
    public string? SearchName { get; init; }
    public bool? IsActive { get; init; }
}
