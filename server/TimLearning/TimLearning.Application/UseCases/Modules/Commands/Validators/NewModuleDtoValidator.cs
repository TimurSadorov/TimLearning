using Microsoft.EntityFrameworkCore;
using TimLearning.Application.UseCases.Modules.Commands.Dto;
using TimLearning.Infrastructure.Interfaces.Db;
using TimLearning.Shared.Validation.Exceptions.Localized;
using TimLearning.Shared.Validation.Validators;

namespace TimLearning.Application.UseCases.Modules.Commands.Validators;

public class NewModuleDtoValidator : IAsyncSimpleValidator<NewModuleDto>
{
    private readonly IAppDbContext _dbContext;

    public NewModuleDtoValidator(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task ValidateAndThrowAsync(NewModuleDto entity, CancellationToken ct = default)
    {
        if (await _dbContext.Courses.AnyAsync(c => c.Id == entity.CourseId, ct) == false)
        {
            LocalizedValidationException.ThrowSimpleTextError(
                $"Курс с идентификатором[{entity.CourseId}] не найден."
            );
        }
    }
}
