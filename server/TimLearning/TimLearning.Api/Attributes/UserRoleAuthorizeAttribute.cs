using Microsoft.AspNetCore.Authorization;
using TimLearning.Domain.Entities.Enums;

namespace TimLearning.Api.Attributes;

public class UserRoleAuthorizeAttribute : AuthorizeAttribute
{
    public UserRoleAuthorizeAttribute(params UserRoleType[] roles)
    {
        Roles = string.Join(",", roles.Select(r => r.ToString()));
    }
}
