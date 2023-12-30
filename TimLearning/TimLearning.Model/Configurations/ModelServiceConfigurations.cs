using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TimLearning.Model.Db;
using TimLearning.Model.Services;
using TimLearning.Shared.Configuration.Extensions;

namespace TimLearning.Model.Configurations;

public static class ModelServiceConfigurations
{
    public static void AddModelServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(
            optionsBuilder =>
                optionsBuilder.UseNpgsql(config.GetRequiredConfig<string>("Db:ConnectionString"))
        );

        services.AddScoped<ExerciseService>();
    }
}
