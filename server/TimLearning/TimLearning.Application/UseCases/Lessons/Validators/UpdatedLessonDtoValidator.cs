using Microsoft.EntityFrameworkCore;
using TimLearning.Application.UseCases.Lessons.Dto;
using TimLearning.Domain.Exceptions;
using TimLearning.Infrastructure.Interfaces.Db;
using TimLearning.Shared.Validation.Exceptions.Localized;
using TimLearning.Shared.Validation.Validators;

namespace TimLearning.Application.UseCases.Lessons.Validators;

public class UpdatedLessonDtoValidator : IAsyncSimpleValidator<UpdatedLessonDto>
{
    private readonly IAppDbContext _dbContext;

    public UpdatedLessonDtoValidator(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task ValidateAndThrowAsync(UpdatedLessonDto entity, CancellationToken ct = default)
    {
        var lesson = await _dbContext.Lessons.FirstOrDefaultAsync(l => l.Id == entity.Id, ct);
        if (lesson is null)
        {
            throw new NotFoundException();
        }

        if (entity.Name is null)
        {
            LocalizedValidationException.ThrowSimpleTextError("Имя урока не может быть пустым.");
        }

        if (entity.ExerciseDto?.Value is not null)
        {
            var exercise = entity.ExerciseDto.Value;
            if (
                await _dbContext.StoredFiles.AnyAsync(f => f.Id == exercise.AppArchiveId, ct)
                is false
            )
            {
                LocalizedValidationException.ThrowSimpleTextError(
                    "Файл приложения первоначально не был сохранен в системе."
                );
            }

            if (
                exercise.RelativePathToInsertCode.Any(
                    d => d.Length > 256 || d.Contains('/') || d.Contains('\\')
                )
            )
            {
                LocalizedValidationException.ThrowSimpleTextError(
                    "В названии директории не должно быть символов / и \\ и длина не должна быть больше 256."
                );
            }
        }
    }
}
