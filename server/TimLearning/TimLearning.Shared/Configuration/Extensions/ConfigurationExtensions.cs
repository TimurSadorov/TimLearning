using Microsoft.Extensions.Configuration;

namespace TimLearning.Shared.Configuration.Extensions;

public static class ConfigurationExtensions
{
    public static TConfig GetRequiredConfig<TConfig>(this IConfiguration config, string sectionName)
    {
        var data = config.GetRequiredSection(sectionName).Get<TConfig>();
        return data
            ?? throw new InvalidOperationException(
                $"Config section[{sectionName}] for type[{typeof(TConfig)}] not set."
            );
    }

    public static TConfig GetRequiredConfig<TConfig>(this IConfiguration configuration)
        where TConfig : IConfigurationOptions
    {
        return configuration.GetRequiredConfig<TConfig>(TConfig.SectionName);
    }

    public static string GetRequiredStringValue(this IConfiguration config, string sectionName)
    {
        return config.GetRequiredConfig<string>(sectionName);
    }
}
