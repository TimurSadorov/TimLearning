using System.ComponentModel.DataAnnotations;
using TimLearning.Shared.Configuration;

namespace TimLearning.Infrastructure.Implementation.Configurations.Options;

public class TimLearningOptions : IConfigurationOptions
{
    public static string SectionName => "TimLearning";

    [Required]
    public required string Url { get; init; }
}
