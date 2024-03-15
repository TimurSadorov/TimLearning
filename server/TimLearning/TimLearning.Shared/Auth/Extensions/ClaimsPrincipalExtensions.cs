using System.Security.Claims;

namespace TimLearning.Shared.Auth.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string GetRequiredFirstValue(this ClaimsPrincipal principal, string claimType)
    {
        return principal.FindFirstValue(claimType)
            ?? throw new InvalidOperationException(
                $"Claim with the type[{claimType}] was not found."
            );
    }

    public static Guid GetRequiredFirstGuid(this ClaimsPrincipal principal, string claimType)
    {
        var guid = principal.GetRequiredFirstValue(claimType);
        return Guid.Parse(guid);
    }
}
