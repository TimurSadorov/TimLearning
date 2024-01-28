using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using TimLearning.Api.ToDo.Requests.Docker;

namespace TimLearning.Api.ToDo.Requests;

public record NewAppRequest
{
    [Required]
    public required IFormFile App { get; init; }

    [Required]
    public required string PathToDockerfile { get; init; }
    
    [Required]
    public required ContainerSettingsRequest ContainerSettings { get; init; }
};
