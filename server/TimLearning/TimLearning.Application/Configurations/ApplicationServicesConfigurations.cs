using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TimLearning.Application.Data.ValueObjects;
using TimLearning.Application.Mediator.Pipelines.RoleAccess;
using TimLearning.Application.Mediator.Pipelines.Transactional;
using TimLearning.Application.Services.CodeReviewServices;
using TimLearning.Application.Services.ExerciseServices;
using TimLearning.Application.Services.LessonServices;
using TimLearning.Application.Services.ModuleServices;
using TimLearning.Application.Services.StudyGroupServices;
using TimLearning.Application.Services.UserProgressServices;
using TimLearning.Application.Services.UserServices;
using TimLearning.Application.Services.UserSolutionServices;
using TimLearning.Application.UseCases.CodeReviewNoteComments.Commands.CreateCodeReviewNoteComment;
using TimLearning.Application.UseCases.CodeReviewNoteComments.Validators;
using TimLearning.Application.UseCases.CodeReviewNotes.Commands.CreateCodeReviewNote;
using TimLearning.Application.UseCases.CodeReviewNotes.Commands.DeleteCodeReviewNote;
using TimLearning.Application.UseCases.CodeReviewNotes.Validators;
using TimLearning.Application.UseCases.Lessons.Dto;
using TimLearning.Application.UseCases.Lessons.Validators;
using TimLearning.Application.UseCases.Modules.Dto;
using TimLearning.Application.UseCases.Modules.Validators;
using TimLearning.Application.UseCases.StudyGroups.Commands.JoinToStudyGroup;
using TimLearning.Application.UseCases.StudyGroups.Dto;
using TimLearning.Application.UseCases.StudyGroups.Validators;
using TimLearning.Application.UseCases.Users.Commands.RegisterUser;
using TimLearning.Application.UseCases.Users.Dto;
using TimLearning.Application.UseCases.Users.Validators;
using TimLearning.Application.Validators.Course;
using TimLearning.Application.Validators.Users;
using TimLearning.Shared.Configuration.Extensions;
using TimLearning.Shared.Services.Archiving;
using TimLearning.Shared.Services.Encryptors.DataEncryptor;
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
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(RegisterUserCommand).Assembly);
            cfg.AddOpenBehavior(typeof(AccessByRolePipelineBehavior<,>));
            cfg.AddOpenBehavior(typeof(TransactionPipelineBehavior<,>));
        });

        services.AddPrivateServices(config);
        services.AddValidators();
    }

    private static void AddPrivateServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDataHasher(config.GetRequiredStringValue("DataEncryptor:SharedKey"));
        services.AddArchivingService();

        services.AddSingleton<IUserDataEncryptor, UserDataEncryptor>();
        services.AddScoped<IUserTokenUpdater, UserTokenUpdater>();
        services.AddSingleton<IUserEmailProvider, UserEmailProvider>();

        services.AddScoped<IModuleOrderService, ModuleOrderService>();

        services.AddScoped<ILessonPositionService, LessonPositionService>();

        services.AddScoped<IExerciseTester, ExerciseTester>();

        services.AddScoped<IUserProgressService, UserProgressService>();

        services.AddScoped<IUserSolutionService, UserSolutionService>();

        services.AddSingleton<IStudyGroupDataEncryptor, StudyGroupDataEncryptor>();

        services.AddScoped<ICodeReviewService, CodeReviewService>();
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
        services.AddScoped<
            ICombinedFluentAndSimpleValidator<NewRecoveringPasswordDto>,
            NewRecoveringPasswordDtoValidator
        >();

        services.AddScoped<IAsyncSimpleValidator<CourseIdValueObject>, CourseIdValidator>();

        services.AddScoped<
            IAsyncSimpleValidator<ModuleOrderChangingDto>,
            ModuleOrderChangingDtoValidator
        >();

        services.AddScoped<IAsyncSimpleValidator<LessonMovementDto>, LessonMovementDtoValidator>();
        services.AddScoped<IAsyncSimpleValidator<UpdatedLessonDto>, UpdatedLessonDtoValidator>();

        services.AddScoped<IAsyncSimpleValidator<NewStudyGroupDto>, NewStudyGroupDtoValidator>();
        services.AddScoped<
            IAsyncSimpleValidator<JoinToStudyGroupCommand>,
            JoinToStudyGroupCommandValidator
        >();

        services.AddScoped<
            IAsyncSimpleValidator<CreateCodeReviewNoteCommand>,
            CreateCodeReviewNoteCommandValidator
        >();
        services.AddScoped<
            IAsyncSimpleValidator<DeleteCodeReviewNoteCommand>,
            DeleteCodeReviewNoteCommandValidator
        >();

        services.AddScoped<
            IAsyncSimpleValidator<CreateCodeReviewNoteCommentCommand>,
            CreateCodeReviewNoteCommentCommandValidator
        >();
    }
}
