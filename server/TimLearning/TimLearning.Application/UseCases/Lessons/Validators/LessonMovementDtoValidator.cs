using Microsoft.EntityFrameworkCore;
using TimLearning.Application.UseCases.Lessons.Dto;
using TimLearning.Domain.Exceptions;
using TimLearning.Infrastructure.Interfaces.Db;
using TimLearning.Shared.Validation.Exceptions.Localized;
using TimLearning.Shared.Validation.Validators;

namespace TimLearning.Application.UseCases.Lessons.Validators;

public class LessonMovementDtoValidator : IAsyncSimpleValidator<LessonMovementDto>
{
    private readonly IAppDbContext _dbContext;

    public LessonMovementDtoValidator(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task ValidateAndThrowAsync(
        LessonMovementDto entity,
        CancellationToken ct = default
    )
    {
        var movableLesson = await _dbContext.Lessons
            .Where(l => l.Id == entity.LessonId)
            .Select(l => new { l.ModuleId, l.IsDeleted })
            .FirstOrDefaultAsync(ct);
        if (movableLesson is null)
        {
            throw new NotFoundException();
        }
        if (movableLesson.IsDeleted)
        {
            LocalizedValidationException.ThrowSimpleTextError("Перемещаемый урок удален.");
        }

        if (entity.NextLessonId is not null)
        {
            if (entity.LessonId == entity.NextLessonId)
            {
                LocalizedValidationException.ThrowSimpleTextError(
                    "Слудющий урок является перемещаемым уроком."
                );
            }

            var nextLesson = await _dbContext.Lessons
                .Where(l => l.ModuleId == movableLesson.ModuleId && l.Id == entity.NextLessonId)
                .Select(l => new { l.IsDeleted })
                .FirstOrDefaultAsync(ct);
            if (nextLesson is null)
            {
                LocalizedValidationException.ThrowSimpleTextError("Следущий урок не найден.");
            }

            if (nextLesson.IsDeleted)
            {
                LocalizedValidationException.ThrowSimpleTextError("Следущий урок удален.");
            }
        }
    }
}
