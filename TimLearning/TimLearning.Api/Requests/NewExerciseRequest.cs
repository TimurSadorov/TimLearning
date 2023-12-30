using System.ComponentModel.DataAnnotations;
using TimLearning.Api.Requests.Docker;

namespace TimLearning.Api.Requests;

public record NewExerciseRequest
{
    [Required]
    public required NewAppRequest NewApp { get; init; }

    [Required]
    public required string Code { get; init; }

    [Required]
    public required string PathToRewriteFile { get; init; }

    public List<ImageSettingsRequest>? Images { get; init; }
}
