using System.ComponentModel.DataAnnotations;

namespace TimLearning.Api.Requests.StudyGroup;

public class JoinToStudyGroupRequest
{
    [Required]
    public required string Signature { get; init; }
}
