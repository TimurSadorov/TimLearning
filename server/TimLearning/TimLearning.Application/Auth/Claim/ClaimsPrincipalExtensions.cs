using System.Security.Claims;
using TimLearning.Shared.Auth.Extensions;

namespace TimLearning.Application.Auth.Claim;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal principal)
    {
        return principal.GetRequiredFirstGuid(ExtendedClaimTypes.Id);
    }
}
