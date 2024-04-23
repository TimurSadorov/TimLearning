using System.ComponentModel.DataAnnotations;
using TimLearning.Shared.Configuration;

namespace TimLearning.Infrastructure.Implementation.Storages.S3;

public class AmazonS3Options : IConfigurationOptions
{
    public static string SectionName => "S3";

    [Required]
    public required string ServiceUrl { get; init; }

    [Required]
    public required string AccessKey { get; init; }

    [Required]
    public required string SecretKey { get; init; }

    [Required]
    public required string BucketName { get; init; }
}
