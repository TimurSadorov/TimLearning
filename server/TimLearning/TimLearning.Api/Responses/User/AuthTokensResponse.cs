using System.ComponentModel.DataAnnotations;

namespace TimLearning.Api.Responses.User;

public record AuthTokensResponse(
    [property: Required] string AccessToken,
    [property: Required] string RefreshToken
);
