using MediatR;
using Microsoft.EntityFrameworkCore;
using TimLearning.Application.Extensions;
using TimLearning.Application.Services.ExerciseServices;
using TimLearning.Application.Services.ExerciseServices.Dto;
using TimLearning.Application.Services.ExerciseServices.Mappers;
using TimLearning.Application.Services.UserProgressServices;
using TimLearning.Application.Services.UserSolutionServices;
using TimLearning.Application.Specifications;
using TimLearning.Application.UseCases.Lessons.Dto;
using TimLearning.Application.UseCases.Lessons.Mappers;
using TimLearning.Domain.Exceptions;
using TimLearning.Infrastructure.Interfaces.Db;

namespace TimLearning.Application.UseCases.Lessons.Command.TestUserLessonExercise;

public class TestUserLessonExerciseCommandHandler
    : IRequestHandler<TestUserLessonExerciseCommand, UserExerciseTestingResultDto>
{
    private readonly IAppDbContext _dbContext;
    private readonly IExerciseTester _exerciseTester;
    private readonly IUserProgressService _userProgressService;
    private readonly IUserSolutionService _userSolutionService;

    public TestUserLessonExerciseCommandHandler(
        IAppDbContext dbContext,
        IExerciseTester exerciseTester,
        IUserProgressService userProgressService,
        IUserSolutionService userSolutionService
    )
    {
        _dbContext = dbContext;
        _exerciseTester = exerciseTester;
        _userProgressService = userProgressService;
        _userSolutionService = userSolutionService;
    }

    public async Task<UserExerciseTestingResultDto> Handle(
        TestUserLessonExerciseCommand request,
        CancellationToken cancellationToken
    )
    {
        var exercise = await _dbContext
            .Lessons.AsNoTracking()
            .Where(l => l.Id == request.LessonId)
            .Where(LessonSpecifications.IsPractical && LessonSpecifications.UserAvailable)
            .Select(l => l.Exercise)
            .FirstOrDefaultAsync(cancellationToken);
        if (exercise is null)
        {
            throw new NotFoundException();
        }

        var testingResult = await _exerciseTester.Test(
            exercise.ToDto(request.Code),
            cancellationToken
        );
        if (testingResult.Status is not ExerciseTestingStatus.Ok)
        {
            return new UserExerciseTestingResultDto(
                testingResult.Status.ToUserTestingStatus(),
                testingResult.ErrorMessage
            );
        }

        await _dbContext.Database.ExecuteInTransactionAsync(
            async () =>
            {
                await _userProgressService.Complete(
                    request.LessonId,
                    request.CallingUserId,
                    cancellationToken
                );

                await _userSolutionService.Create(
                    request.CallingUserId,
                    exercise.Id,
                    request.Code,
                    cancellationToken
                );
            },
            cancellationToken
        );

        return new UserExerciseTestingResultDto(UserExerciseTestingStatus.Ok, null);
    }
}
