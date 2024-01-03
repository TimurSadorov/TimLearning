using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TimLearning.Model.Db;
using TimLearning.Model.Dto.User;
using TimLearning.Model.Services;
using TimLearning.Model.Validators;
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
        services.AddScoped<IUserService, UserService>();

        services.AddScoped<IValidator<NewUserDto>, NewUserDtoValidator>();
    }
}
