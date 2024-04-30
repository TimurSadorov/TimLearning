using System.ComponentModel.DataAnnotations;
using TimLearning.Shared.Configuration;

namespace TimLearning.DockerManager.ApiClient;

public class DockerManagerApiOptions : IConfigurationOptions
{
    public static string SectionName => "DockerManagerApi";

    [Required]
    public required string Url { get; init; }
}
