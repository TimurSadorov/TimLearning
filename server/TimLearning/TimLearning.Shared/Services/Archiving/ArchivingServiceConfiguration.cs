using Microsoft.Extensions.DependencyInjection;

namespace TimLearning.Shared.Services.Archiving;

public static class ArchivingServiceConfiguration
{
    public static IServiceCollection AddArchivingService(this IServiceCollection services)
    {
        services.AddSingleton<IArchivingService, ArchivingService>();

        return services;
    }
}
