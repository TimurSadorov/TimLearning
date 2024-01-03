using System.Security.Claims;

namespace TimLearning.Shared.AspNet.Auth.Claim;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal principal)
    {
        var userId =
            principal.FindFirstValue(ExtendedClaimTypes.Id)
            ?? throw new InvalidOperationException(
                $"Claim with the type[{ExtendedClaimTypes.Id}] was not found."
            );

        return Guid.Parse(userId);
    }
}
