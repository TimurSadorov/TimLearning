using TimLearning.Domain.Entities.Enums;

namespace TimLearning.Domain.Access;

public static class AccessGroup
{
    public static readonly UserRoleType[] Everyone =
        [UserRoleType.User, UserRoleType.Mentor, UserRoleType.ContentCreator, UserRoleType.Admin];
}