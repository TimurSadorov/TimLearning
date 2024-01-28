using System.Security.Cryptography;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TimLearning.Application.Services.DataEncryptors.PasswordEncryptor;
using TimLearning.Application.Services.DataEncryptors.UserDataEncryptor;
using TimLearning.Application.ToDoServices;
using TimLearning.Application.UseCases.Users.Commands.RegisterUser;
using TimLearning.Application.UseCases.Users.Dto;
using TimLearning.Application.Validators.User;
using TimLearning.Shared.Configuration.Extensions;
using TimLearning.Shared.Services.Encryptors.DataEncryptor;
using TimLearning.Shared.Validation.Validators;

namespace TimLearning.Application.Configurations;

public static class ApplicationServicesConfigurations
{
    public static void AddAllApplicationServices(
        this IServiceCollection services,
        IConfiguration config
    )
    {
        services.AddMediatR(
            cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterUserCommand).Assembly)
        );

        services.AddPrivateServices(config);
        services.AddValidators();
    }

    private static void AddPrivateServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDataEncryptors(config);

        services.AddToDoServices();
    }

    private static void AddDataEncryptors(this IServiceCollection services, IConfiguration config)
    {
        services.AddDataHasher(config.GetRequiredStringValue("DataEncryptor:SharedKey"));
        services.AddSingleton<IPasswordEncryptor>(new PasswordEncryptor(SHA512.Create()));
        services.AddSingleton<IUserDataEncryptor, UserDataEncryptor>();
    }

    // TODO: services for future use
    private static void AddToDoServices(this IServiceCollection services)
    {
        services.AddScoped<ExerciseService>();
    }

    private static void AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<NewUserDto>, NewUserDtoValidator>();
        services.AddScoped<
            IAsyncSimpleValidator<UserEmailConfirmationDto>,
            UserEmailConfirmationDtoValidator
        >();
    }
}
