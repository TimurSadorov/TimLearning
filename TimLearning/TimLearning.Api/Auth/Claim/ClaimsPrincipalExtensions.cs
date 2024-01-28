using System.Security.Claims;
using TimLearning.Shared.Auth.Extensions;

namespace TimLearning.Api.Auth.Claim;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal principal)
    {
        return principal.FindRequiredFirstGuid(ExtendedClaimTypes.Id);
    }
}
