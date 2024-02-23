using System.ComponentModel.DataAnnotations;
using TimLearning.Shared.Configuration;

namespace TimLearning.Infrastructure.Implementation.Configurations.Options;

public class TimLearningSiteOptions : IConfigurationOptions
{
    public static string SectionName => "TimLearningSite";

    [Required]
    public required string Url { get; init; }
}
