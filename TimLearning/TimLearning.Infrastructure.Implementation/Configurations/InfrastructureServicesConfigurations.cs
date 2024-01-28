using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TimLearning.Infrastructure.Implementation.Configurations.Options;
using TimLearning.Infrastructure.Implementation.Db;
using TimLearning.Infrastructure.Implementation.Factories.Link;
using TimLearning.Infrastructure.Implementation.Services.Email;
using TimLearning.Infrastructure.Interfaces.Db;
using TimLearning.Infrastructure.Interfaces.Factories.Link;
using TimLearning.Infrastructure.Interfaces.Services.Email;
using TimLearning.Shared.Configuration.Extensions;

namespace TimLearning.Infrastructure.Implementation.Configurations;

public static class InfrastructureServicesConfigurations
{
    public static void AddAllInfrastructureServices(
        this IServiceCollection services,
        IConfiguration config
    )
    {
        services.AddAllOptions();

        services.AddAppDbContext(config);

        services.AddServices();
        services.AddFactories();
    }

    private static void AddAllOptions(this IServiceCollection services)
    {
        services.AddRequiredOptions<TimLearningOptions>();
        services.AddRequiredOptions<MailOptions>();
    }

    private static void AddAppDbContext(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<IAppDbContext, AppAppDbContext>(builder =>
        {
            builder.UseNpgsql(config.GetRequiredStringValue("DbConnectionStrings:Postgres"));
        });
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IEmailService, EmailService>();
    }

    private static void AddFactories(this IServiceCollection services)
    {
        services.AddSingleton<ITimLearningLinkFactory, TimLearningLinkFactory>();
    }
}
