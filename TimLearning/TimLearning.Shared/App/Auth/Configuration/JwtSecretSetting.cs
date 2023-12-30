using TimLearning.Shared.Configuration;
using TimLearning.Shared.Extensions;

namespace TimLearning.Shared.App.Auth.Configuration;

public class JwtSecretSetting : IConfigurationSettings
{
    public static string SectionName => "JwtSecret";

    public required string Key { get; init; }

    public byte[] KeyInByte => Key.EncodeUTF8();
}
