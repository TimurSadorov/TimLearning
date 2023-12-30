using System.ComponentModel.DataAnnotations;
using TimLearning.Shared.Configuration;

namespace TimLearning.Model.S3.Configuration;

public sealed class AmazonS3Settings : IConfigurationSettings
{
    public static string SectionName => "S3";

    [Required]
    public required string ServiceUrl { get; set; }

    [Required]
    public required string AccessKey { get; set; }

    [Required]
    public required string SecretKey { get; set; }

    [Required]
    public required string ExercisesBucketName { get; set; }
}
