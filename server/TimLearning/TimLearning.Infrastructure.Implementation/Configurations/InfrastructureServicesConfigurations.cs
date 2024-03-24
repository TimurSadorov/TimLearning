using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TimLearning.Infrastructure.Implementation.Configurations.Options;
using TimLearning.Infrastructure.Implementation.Db;
using TimLearning.Infrastructure.Implementation.Factories.Link;
using TimLearning.Infrastructure.Implementation.Providers.Clock;
using TimLearning.Infrastructure.Implementation.Services.Email;
using TimLearning.Infrastructure.Interfaces.Db;
using TimLearning.Infrastructure.Interfaces.Factories.Link;
using TimLearning.Infrastructure.Interfaces.Providers.Clock;
using TimLearning.Infrastructure.Interfaces.Services.Email;
using TimLearning.Shared.Configuration.Extensions;

namespace TimLearning.Infrastructure.Implementation.Configurations;

public static class InfrastructureServicesConfigurations
{
    public static void AddAllInfrastructureServices(
        this IServiceCollection services,
        IConfiguration config,
        Action<DbContextOptionsBuilder>? dbOptionsBuilder = null
    )
    {
        services.AddAllOptions();

        services.AddAppDbContext(config, dbOptionsBuilder);

        services.AddServices();
    }

    private static void AddAllOptions(this IServiceCollection services)
    {
        services.AddRequiredOptions<TimLearningSiteOptions>();
        services.AddRequiredOptions<MailOptions>();
    }

    private static void AddAppDbContext(
        this IServiceCollection services,
        IConfiguration config,
        Action<DbContextOptionsBuilder>? dbOptionsBuilder = null
    )
    {
        services.AddDbContext<IAppDbContext, AppAppDbContext>(builder =>
        {
            builder.UseNpgsql(
                config.GetRequiredStringValue("DbConnectionStrings:Postgres"),
                psqlOptions => psqlOptions.EnableRetryOnFailure()
            );

            dbOptionsBuilder?.Invoke(builder);
        });
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IEmailService, EmailService>();

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddSingleton<ITimLearningLinkFactory, TimLearningLinkFactory>();
    }
}
