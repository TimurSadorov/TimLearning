using System.ComponentModel.DataAnnotations;

namespace TimLearning.Api.Responses.StudyGroup;

public record GetLinkToJoinToStudyGroupResponse([property: Required] string LinkToJoin);
