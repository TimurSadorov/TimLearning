using System.ComponentModel.DataAnnotations;
using TimLearning.Api.ToDo.Requests.Docker;

namespace TimLearning.Api.ToDo.Requests;

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
