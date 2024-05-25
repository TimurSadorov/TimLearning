using Microsoft.EntityFrameworkCore;
using TimLearning.Application.Specifications;
using TimLearning.Application.UseCases.StudyGroups.Dto;
using TimLearning.Infrastructure.Interfaces.Db;
using TimLearning.Shared.Extensions;
using TimLearning.Shared.Validation.Exceptions.Localized;
using TimLearning.Shared.Validation.Validators;

namespace TimLearning.Application.UseCases.StudyGroups.Validators;

public class NewStudyGroupDtoValidator : IAsyncSimpleValidator<NewStudyGroupDto>
{
    private readonly IAppDbContext _dbContext;

    public NewStudyGroupDtoValidator(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task ValidateAndThrowAsync(NewStudyGroupDto entity, CancellationToken ct = default)
    {
        if (entity.Name.HasText() == false)
        {
            LocalizedValidationException.ThrowSimpleTextError(
                "Название группы не должно быть пустым."
            );
        }

        if (
            await _dbContext
                .Courses.Where(c => c.Id == entity.CourseId)
                .Where(CourseSpecifications.UserAvailable)
                .AnyAsync(ct) == false
        )
        {
            LocalizedValidationException.ThrowSimpleTextError("Пользовательский курс не найден.");
        }
    }
}
