using System.ComponentModel.DataAnnotations;
using TimLearning.Api.Requests.Docker;

namespace TimLearning.Api.Requests;

public record NewAppRequest
{
    [Required]
    public required IFormFile App { get; init; }

    [Required]
    public required string PathToDockerfile { get; init; }
    
    [Required]
    public required ContainerSettingsRequest ContainerSettings { get; init; }
};
