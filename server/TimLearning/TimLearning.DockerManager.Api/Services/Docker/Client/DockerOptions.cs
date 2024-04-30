using System.ComponentModel.DataAnnotations;
using TimLearning.Shared.Configuration;

namespace TimLearning.DockerManager.Api.Services.Docker.Client;

public class DockerOptions : IConfigurationOptions
{
    public static string SectionName => "Docker";

    [Required]
    public required string Url { get; init; }
}
