using System.Security.Cryptography;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TimLearning.Application.Configurations.Options;
using TimLearning.Application.Data.ValueObjects;
using TimLearning.Application.Services.UserServices;
using TimLearning.Application.ToDoServices;
using TimLearning.Application.UseCases.Users.Commands.RegisterUser;
using TimLearning.Application.UseCases.Users.Dto;
using TimLearning.Application.UseCases.Users.Validators;
using TimLearning.Application.Validators.Users;
using TimLearning.Shared.Configuration.Extensions;
using TimLearning.Shared.Services.Encryptors.DataEncryptor;
using TimLearning.Shared.Services.Encryptors.PasswordEncryptor;
using TimLearning.Shared.Validation.FluentValidator.Validators;
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

        services.AddAppOptions();

        services.AddPrivateServices(config);
        services.AddValidators();
    }

    private static void AddAppOptions(this IServiceCollection services)
    {
        services.AddRequiredOptions<JwtSecretOptions>();
    }

    private static void AddPrivateServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDataHasher(config.GetRequiredStringValue("DataEncryptor:SharedKey"));

        services.AddSingleton<IUserPasswordService>(
            new UserPasswordService(new PasswordEncryptor(SHA512.Create()))
        );
        services.AddSingleton<IUserDataEncryptor, UserDataEncryptor>();
        services.AddSingleton<IJwtTokensGenerator, JwtTokenGenerator>();
        services.AddSingleton<IUserTokenGenerator, UserTokenGenerator>();
        services.AddScoped<IUserTokenUpdater, UserTokenUpdater>();
        services.AddSingleton<IUserEmailProvider, UserEmailProvider>();

        services.AddToDoServices();
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
        services.AddScoped<
            IAsyncSimpleValidator<NewUserEmailConfirmationDto>,
            NewUserEmailConfirmationDtoValidator
        >();
        services.AddScoped<IAsyncSimpleValidator<AuthorizationDto>, AuthorizationDtoValidator>();
        services.AddScoped<
            IAsyncSimpleValidator<RefreshableTokenDto>,
            RefreshableTokenDtoValidator
        >();
        services.AddScoped<IAsyncSimpleValidator<UserEmailValueObject>, UserEmailValidator>();
        services.AddSingleton<IValidator<UserPasswordValueObject>, UserPasswordValidator>();
        services.AddScoped<
            ICombinedFluentAndSimpleValidator<NewRecoveringPasswordDto>,
            NewRecoveringPasswordDtoValidator
        >();
    }
}
