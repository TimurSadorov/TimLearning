using MediatR;
using Microsoft.EntityFrameworkCore;
using TimLearning.Application.Services.ExerciseServices;
using TimLearning.Application.Services.ExerciseServices.Dto;
using TimLearning.Application.UseCases.Lessons.Dto;
using TimLearning.Domain.Entities;
using TimLearning.Infrastructure.Interfaces.Db;
using TimLearning.Shared.Validation.Validators;

namespace TimLearning.Application.UseCases.Lessons.Command.UpdateLesson;

public class UpdateLessonCommandHandler
    : IRequestHandler<UpdateLessonCommand, LessonUpdatingResultDto>
{
    private readonly IAppDbContext _dbContext;
    private readonly IExerciseTester _exerciseTester;
    private readonly IAsyncSimpleValidator<UpdatedLessonDto> _validator;

    public UpdateLessonCommandHandler(
        IAppDbContext dbContext,
        IExerciseTester exerciseTester,
        IAsyncSimpleValidator<UpdatedLessonDto> validator
    )
    {
        _dbContext = dbContext;
        _exerciseTester = exerciseTester;
        _validator = validator;
    }

    public async Task<LessonUpdatingResultDto> Handle(
        UpdateLessonCommand request,
        CancellationToken cancellationToken
    )
    {
        var dto = request.Dto;
        await _validator.ValidateAndThrowAsync(dto, cancellationToken);

        var lesson = await _dbContext
            .Lessons.Include(l => l.Exercise)
            .FirstAsync(l => l.Id == dto.Id, cancellationToken);

        if (dto.ExerciseDto is not null)
        {
            var exerciseDto = dto.ExerciseDto.Value;
            if (exerciseDto is not null)
            {
                var testingResult = await _exerciseTester.Test(exerciseDto, cancellationToken);
                if (testingResult.Status is not ExerciseTestingStatus.Ok)
                {
                    return new LessonUpdatingResultDto(false, testingResult);
                }
            }

            UpdateExercise(lesson, exerciseDto);
        }
        if (dto.Name is not null)
        {
            lesson.Name = dto.Name;
        }
        if (dto.Text is not null)
        {
            lesson.Text = dto.Text;
        }
        if (dto.IsDraft is not null)
        {
            lesson.IsDraft = dto.IsDraft.Value;
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new LessonUpdatingResultDto(true, null);
    }

    private void UpdateExercise(Lesson lesson, ExerciseDto? exerciseDto)
    {
        var exercise = lesson.Exercise;
        if (exerciseDto is null)
        {
            // TODO: dont remove exercise, otherwise, everything connected with it will be deleted
            if (exercise is not null)
            {
                _dbContext.Remove(exercise);
            }
        }
        else
        {
            if (exercise is null)
            {
                lesson.Exercise = new Exercise
                {
                    LessonId = lesson.Id,
                    AppArchiveId = exerciseDto.AppArchiveId,
                    AppContainerData = exerciseDto.AppContainerData,
                    RelativePathToDockerfile = exerciseDto.RelativePathToDockerfile,
                    RelativePathToInsertCode = exerciseDto.RelativePathToInsertCode,
                    StandardCode = exerciseDto.InsertableCode,
                    ServiceApps = exerciseDto.ServiceApps
                };
            }
            else
            {
                exercise.AppArchiveId = exerciseDto.AppArchiveId;
                exercise.AppContainerData = exerciseDto.AppContainerData;
                exercise.RelativePathToDockerfile = exerciseDto.RelativePathToDockerfile;
                exercise.RelativePathToInsertCode = exerciseDto.RelativePathToInsertCode;
                exercise.StandardCode = exerciseDto.InsertableCode;
                exercise.ServiceApps = exerciseDto.ServiceApps;
            }
        }
    }
}
