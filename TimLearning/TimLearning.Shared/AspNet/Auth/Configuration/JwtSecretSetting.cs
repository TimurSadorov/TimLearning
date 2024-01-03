using System.ComponentModel.DataAnnotations;
using TimLearning.Shared.Configuration;
using TimLearning.Shared.Extensions;

namespace TimLearning.Shared.AspNet.Auth.Configuration;

public class JwtSecretSetting : IConfigurationSettings
{
    public static string SectionName => "JwtSecret";

    [Required]
    public required string Key { get; init; }

    public byte[] KeyInByte => Key.EncodeUTF8();
}
