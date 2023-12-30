using Microsoft.Extensions.Configuration;

namespace TimLearning.Shared.Configuration.Extensions;

public static class ConfigurationExtensions
{
    public static TSettings GetRequiredSettings<TSettings>(this IConfiguration configuration)
        where TSettings : IConfigurationSettings
    {
        return configuration.GetRequiredConfig<TSettings>(TSettings.SectionName);
    }

    public static TConfig GetRequiredConfig<TConfig>(this IConfiguration config, string sectionName)
    {
        var data = config.GetValue<TConfig>(sectionName);
        return data
            ?? throw new InvalidOperationException(
                $"Config section[{sectionName}] with type[{typeof(TConfig)}] not set."
            );
    }
}
