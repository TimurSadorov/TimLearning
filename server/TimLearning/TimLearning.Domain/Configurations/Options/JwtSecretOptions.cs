using System.ComponentModel.DataAnnotations;
using TimLearning.Shared.Configuration;
using TimLearning.Shared.Extensions;

namespace TimLearning.Domain.Configurations.Options;

public class JwtSecretOptions : IConfigurationOptions
{
    public static string SectionName => "JwtSecret";

    [Required]
    public required string Key { get; init; }

    public byte[] KeyInByte => Key.EncodeUTF8();
}
