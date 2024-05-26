using System.Security.Cryptography;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TimLearning.Domain.Configurations.Options;
using TimLearning.Domain.Data.ValueObjects;
using TimLearning.Domain.Services.LessonService;
using TimLearning.Domain.Services.UserServices;
using TimLearning.Domain.Validators;
using TimLearning.Shared.Configuration.Extensions;
using TimLearning.Shared.Services.Encryptors.PasswordEncryptor;
using TimLearning.Shared.Validation.Validators;

namespace TimLearning.Domain.Configurations;

public static class ApplicationServicesConfigurations
{
    public static void AddAllDomainServices(this IServiceCollection services)
    {
        services.AddServices();
        services.AddDomainOptions();
        services.AddValidators();
    }

    private static void AddDomainOptions(this IServiceCollection services)
    {
        services.AddRequiredOptions<JwtSecretOptions>();
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddSingleton<ITokenGenerator, TokenGenerator>();
        services.AddSingleton<IUserTokenService, UserTokenService>();
        services.AddSingleton<IUserPasswordService>(
            new UserPasswordService(new PasswordEncryptor(SHA512.Create()))
        );

        services.AddSingleton<ILessonOrderService, LessonOrderService>();
    }

    private static void AddValidators(this IServiceCollection services)
    {
        services.AddSingleton<IValidator<UserPasswordValueObject>, UserPasswordValidator>();

        services.AddSingleton<
            ISimpleValidator<CodeReviewNoteCommentTextValueObject>,
            CodeReviewNoteCommentTextValidator
        >();
    }
}
