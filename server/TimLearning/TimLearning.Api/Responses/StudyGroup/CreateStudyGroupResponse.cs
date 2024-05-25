using System.ComponentModel.DataAnnotations;

namespace TimLearning.Api.Responses.StudyGroup;

public record CreateStudyGroupResponse([property: Required] Guid Id);
